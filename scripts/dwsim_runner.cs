using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

// Compile against framework libraries only; load the user's DWSIM installation at runtime.
internal static class DwsimRunner
{
    private static string install;
    private static readonly Dictionary<string, object> report = new Dictionary<string, object>();
    private static readonly List<object> passes = new List<object>();
    private static readonly List<string> cleanupWarnings = new List<string>();

    [STAThread]
    private static int Main(string[] args)
    {
        if (args.Length != 3) return 2;
        install = args[0];
        string output = args[1];
        string source = args[2];
        report["schema_version"] = 1;
        report["status"] = "failed";
        report["stage"] = "environment";
        report["started_utc"] = DateTime.UtcNow.ToString("o");
        report["mode"] = source.Length == 0 ? "minimum-water-mixer" : "load-and-calculate";
        report["passes"] = passes;
        report["cleanup_warnings"] = cleanupWarnings;
        report["engineering_acceptance"] = "not_assessed";
        dynamic manager = null;
        dynamic model = null;
        try
        {
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
            Environment.SetEnvironmentVariable("PATH", install + ";" + Environment.GetEnvironmentVariable("PATH"));
            var assembly = Assembly.LoadFrom(Path.Combine(install, "DWSIM.Automation.dll"));
            report["automation_version"] = assembly.GetName().Version.ToString();
            report["automation_sha256"] = HashFile(assembly.Location);
            manager = Activator.CreateInstance(assembly.GetType("DWSIM.Automation.Automation3", true));
            report["dwsim_version"] = (string)manager.GetVersion();
            bool minimum = source.Length == 0;
            if (minimum)
            {
                report["stage"] = "build";
                model = BuildMinimum(manager);
                report["assumptions"] = new[] {
                    "Environment demonstration only; not a project design basis.",
                    "Pure water, Steam Tables (IAPWS-IF97), adiabatic once-through mixer.",
                    "Feeds: 1 kg/s at 300 K and 2 kg/s at 340 K; both at 101325 Pa."
                };
            }
            else
            {
                report["stage"] = "load";
                report["source_sha256_before"] = HashFile(source);
                string input = Path.Combine(output, "input" + Path.GetExtension(source));
                File.Copy(source, input, false);
                report["input_copy"] = Path.GetFileName(input);
                if (HashFile(input) != (string)report["source_sha256_before"])
                    throw new InvalidDataException("Input changed while copying; retry with a stable source.");
                model = manager.LoadFlowsheet(input);
            }

            report["stage"] = "calculate";
            var first = Calculate(manager, model, minimum, "initial");
            passes.Add(first);
            RequirePassed(first);
            report["stage"] = "save";
            string saved = Path.Combine(output, "workcopy.dwxmz");
            manager.SaveFlowsheet(model, saved, true);
            if (!File.Exists(saved) || new FileInfo(saved).Length == 0)
                throw new IOException("DWSIM did not save a nonempty model.");
            Release(model);
            model = null;
            // A new manager also avoids reusing a live flowsheet or property-package state.
            Release(manager);
            manager = Activator.CreateInstance(assembly.GetType("DWSIM.Automation.Automation3", true));
            report["stage"] = "reload";
            model = manager.LoadFlowsheet(saved);
            report["stage"] = "recalculate";
            var second = Calculate(manager, model, minimum, "saved-reload");
            passes.Add(second);
            RequirePassed(second);
            report["stage"] = "roundtrip";
            CheckRoundtrip(first, second);
            report["stage"] = "export";
            ExportStreams(Path.Combine(output, "key_streams.csv"), (List<Dictionary<string, object>>)second["streams"]);
            report["workcopy"] = "workcopy.dwxmz";
            report["workcopy_sha256"] = HashFile(saved);
            report["status"] = "passed";
            report["stage"] = "complete";
        }
        catch (Exception ex)
        {
            report["error"] = ex.ToString();
        }
        finally
        {
            Release(model);
            Release(manager);
            if (source.Length != 0 && report.ContainsKey("source_sha256_before"))
            {
                try
                {
                    report["source_sha256_after"] = HashFile(source);
                    if ((string)report["source_sha256_before"] != (string)report["source_sha256_after"])
                    {
                        report["status"] = "failed";
                        report["stage"] = "source-integrity";
                        report["error"] = "Source changed during the run; do not accept this result.";
                    }
                }
                catch (Exception ex)
                {
                    report["status"] = "failed";
                    report["stage"] = "source-integrity";
                    report["error"] = ex.Message;
                }
            }
            report["completed_utc"] = DateTime.UtcNow.ToString("o");
            File.WriteAllText(Path.Combine(output, "run-status.json"),
                new JavaScriptSerializer().Serialize(report), new UTF8Encoding(false));
        }
        return (string)report["status"] == "passed" ? 0 : 1;
    }

    private static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
    {
        string name = new AssemblyName(args.Name).Name;
        foreach (string dir in new[] { install, Path.Combine(install, "extenders") })
            foreach (string suffix in new[] { ".dll", ".exe" })
            {
                string path = Path.Combine(dir, name + suffix);
                if (File.Exists(path)) return Assembly.LoadFrom(path);
            }
        return null;
    }

    private static dynamic BuildMinimum(dynamic manager)
    {
        dynamic sim = manager.CreateFlowsheet();
        try
        {
            sim.AddCompound("Water");
            sim.CreateAndAddPropertyPackage("Steam Tables (IAPWS-IF97)");
            Type types = Assembly.LoadFrom(Path.Combine(install, "DWSIM.Interfaces.dll"))
                .GetType("DWSIM.Interfaces.Enums.GraphicObjects.ObjectType", true);
            dynamic streamType = Enum.Parse(types, "MaterialStream");
            dynamic mixerType = Enum.Parse(types, "Mixer");
            dynamic a = sim.AddObject(streamType, 40, 40, "feed-cold");
            dynamic b = sim.AddObject(streamType, 40, 140, "feed-warm");
            dynamic product = sim.AddObject(streamType, 300, 90, "product");
            dynamic mixer = sim.AddObject(mixerType, 170, 90, "mixer");
            mixer.ConnectFeedMaterialStream(a, 0);
            mixer.ConnectFeedMaterialStream(b, 1);
            mixer.ConnectProductMaterialStream(product, 0);
            a.SetTemperature(300.0);
            a.SetPressure(101325.0);
            a.SetMassFlow(1.0);
            b.SetTemperature(340.0);
            b.SetPressure(101325.0);
            b.SetMassFlow(2.0);
            a.SetOverallComposition(new double[] { 1.0 });
            b.SetOverallComposition(new double[] { 1.0 });
            return sim;
        }
        catch { Release(sim); throw; }
    }

    private static Dictionary<string, object> Calculate(dynamic manager, dynamic sim, bool minimum, string name)
    {
        var errors = new List<string>();
        sim.ResetCalculationStatus();
        // Automation3.CalculateFlowsheet2 is void in 9.0.5; CalculateFlowsheet4 preserves solver errors.
        foreach (Exception error in (IEnumerable)manager.CalculateFlowsheet4(sim)) errors.Add(error.ToString());
        var rows = new List<Dictionary<string, object>>();
        var inventory = new List<string>();
        foreach (dynamic item in (IEnumerable)sim.SimulationObjects.Values)
        {
            inventory.Add((string)item.Name + ":" + item.GetType().FullName);
            if (item.GetType().FullName != "DWSIM.Thermodynamics.Streams.MaterialStream") continue;
            var row = new Dictionary<string, object> {
                {"id", (string)item.Name}, {"tag", (string)item.GraphicObject.Tag},
                {"temperature_K", (double)item.GetTemperature()}, {"pressure_Pa", (double)item.GetPressure()},
                {"mass_flow_kg_s", (double)item.GetMassFlow()}, {"mass_enthalpy_kJ_kg", (double)item.GetMassEnthalpy()}
            };
            foreach (string key in new[] { "temperature_K", "pressure_Pa", "mass_flow_kg_s", "mass_enthalpy_kJ_kg" })
                if (Double.IsNaN((double)row[key]) || Double.IsInfinity((double)row[key]))
                {
                    errors.Add("Non-finite stream value: " + row["tag"] + "/" + key);
                    row[key] = null;
                }
            if ((row["temperature_K"] != null && (double)row["temperature_K"] <= 0) ||
                (row["pressure_Pa"] != null && (double)row["pressure_Pa"] <= 0) ||
                (row["mass_flow_kg_s"] != null && (double)row["mass_flow_kg_s"] < 0))
                errors.Add("Invalid absolute temperature, pressure, or negative mass flow: " + row["tag"]);
            rows.Add(row);
        }
        if (rows.Count == 0) errors.Add("No material streams were available for verification.");
        rows = rows.OrderBy(row => (string)row["id"], StringComparer.Ordinal).ToList();
        var result = new Dictionary<string, object> {
            {"name", name}, {"solved", (bool)sim.Solved}, {"errors", errors},
            {"streams", rows}, {"inventory", inventory.OrderBy(x => x, StringComparer.Ordinal).ToArray()}
        };
        if (minimum && errors.Count == 0)
        {
            var cold = rows.Single(r => (string)r["tag"] == "feed-cold");
            var warm = rows.Single(r => (string)r["tag"] == "feed-warm");
            var product = rows.Single(r => (string)r["tag"] == "product");
            double mass = (double)product["mass_flow_kg_s"] - 3.0;
            double energy = (double)cold["mass_enthalpy_kJ_kg"] + 2 * (double)warm["mass_enthalpy_kJ_kg"]
                - (double)product["mass_flow_kg_s"] * (double)product["mass_enthalpy_kJ_kg"];
            result["mass_residual_kg_s"] = mass;
            result["energy_residual_kW"] = energy;
            result["mass_tolerance_kg_s"] = 1e-6;
            result["energy_tolerance_kW"] = 1e-3;
            double temperature = (double)product["temperature_K"];
            if (Math.Abs(mass) > 1e-6 || Math.Abs(energy) > 1e-3 || temperature < 300 || temperature > 340)
                errors.Add("Water-mixer balance or outlet-temperature check failed.");
        }
        result["passed"] = (bool)result["solved"] && errors.Count == 0;
        return result;
    }

    private static void RequirePassed(Dictionary<string, object> pass)
    {
        if (!(bool)pass["passed"]) throw new InvalidDataException("Solver or result checks failed; inspect passes.");
    }

    private static void CheckRoundtrip(Dictionary<string, object> a, Dictionary<string, object> b)
    {
        if (!((string[])a["inventory"]).SequenceEqual((string[])b["inventory"]))
            throw new InvalidDataException("Object inventory changed after save/reload.");
        var before = (List<Dictionary<string, object>>)a["streams"];
        var after = (List<Dictionary<string, object>>)b["streams"];
        if (before.Count != after.Count) throw new InvalidDataException("Stream count changed after save/reload.");
        for (int i = 0; i < before.Count; i++)
            foreach (string key in new[] { "temperature_K", "pressure_Pa", "mass_flow_kg_s", "mass_enthalpy_kJ_kg" })
            {
                double x = (double)before[i][key], y = (double)after[i][key];
                if ((string)before[i]["id"] != (string)after[i]["id"] || Math.Abs(x - y) > 1e-6 * Math.Max(1, Math.Abs(x)))
                    throw new InvalidDataException("Stream result changed after save/reload: " + before[i]["tag"] + "/" + key);
            }
        report["roundtrip_relative_tolerance"] = 1e-6;
    }

    private static void ExportStreams(string path, List<Dictionary<string, object>> rows)
    {
        string[] keys = { "id", "tag", "temperature_K", "pressure_Pa", "mass_flow_kg_s", "mass_enthalpy_kJ_kg" };
        using (var writer = new StreamWriter(path, false, new UTF8Encoding(false)))
        {
            writer.WriteLine(String.Join(",", keys));
            foreach (var row in rows)
                writer.WriteLine(String.Join(",", keys.Select(key => "\"" + Convert.ToString(row[key], CultureInfo.InvariantCulture).Replace("\"", "\"\"") + "\"")));
        }
    }

    private static string HashFile(string path)
    {
        using (var sha = SHA256.Create())
        using (var stream = File.OpenRead(path))
            return BitConverter.ToString(sha.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
    }

    private static void Release(dynamic instance)
    {
        if (instance == null) return;
        try { instance.ReleaseResources(); }
        catch (Exception ex) { cleanupWarnings.Add(ex.Message); }
    }
}
