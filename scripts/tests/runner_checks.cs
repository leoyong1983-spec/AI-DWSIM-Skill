using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

// Fakes exercise evidence gates only. They are not DWSIM runtime evidence.
namespace DWSIM.Thermodynamics.Streams
{
    public class MaterialStream
    {
        public string Name = "stream-1";
        public FakeGraphic GraphicObject = new FakeGraphic();
        public double Temperature = 300;
        public double Pressure = 101325;
        public double Flow = 1;
        public double Enthalpy = 100;
        public double GetTemperature() { return Temperature; }
        public double GetPressure() { return Pressure; }
        public double GetMassFlow() { return Flow; }
        public double GetMassEnthalpy() { return Enthalpy; }
    }
}
public class FakeGraphic { public string Tag = "water"; }
public class FakeModel
{
    public bool Solved = true;
    public bool ResetCalled;
    public Dictionary<string, object> SimulationObjects = new Dictionary<string, object>();
    public void ResetCalculationStatus() { ResetCalled = true; }
}
public class FakeManager
{
    public List<Exception> Errors = new List<Exception>();
    public List<Exception> CalculateFlowsheet4(FakeModel model) { return Errors; }
}
internal static class RunnerChecks
{
    private static int count;
    private static void Assert(bool condition, string name)
    {
        if (!condition) throw new Exception("FAILED: " + name);
        count++;
    }
    private static Dictionary<string, object> Calculate(FakeModel model, FakeManager manager)
    {
        return (Dictionary<string, object>)typeof(DwsimRunner)
            .GetMethod("Calculate", BindingFlags.Static | BindingFlags.NonPublic)
            .Invoke(null, new object[] { manager, model, false, "test" });
    }
    public static int Main()
    {
        var model = new FakeModel();
        var manager = new FakeManager();
        var stream = new DWSIM.Thermodynamics.Streams.MaterialStream();
        model.SimulationObjects.Add(stream.Name, stream);
        Assert((bool)Calculate(model, manager)["passed"], "valid finite solved case");
        Assert(model.ResetCalled, "reset old calculation flags");
        model.Solved = false;
        Assert(!(bool)Calculate(model, manager)["passed"], "unsolved case rejected");
        model.Solved = true;
        manager.Errors.Add(new Exception("Non-convergence"));
        Assert(!(bool)Calculate(model, manager)["passed"], "solver errors rejected even with solved flag");
        manager.Errors.Clear();
        foreach (double value in new[] { Double.NaN, Double.PositiveInfinity, Double.NegativeInfinity, 0.0, -1.0 })
        {
            stream.Temperature = value;
            var result = Calculate(model, manager);
            Assert(!(bool)result["passed"], "invalid temperature " + value);
            new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(result);
        }
        stream.Temperature = 300;
        stream.Pressure = 0;
        Assert(!(bool)Calculate(model, manager)["passed"], "zero absolute pressure rejected");
        stream.Pressure = 101325;
        stream.Flow = -1;
        Assert(!(bool)Calculate(model, manager)["passed"], "negative mass flow rejected");
        stream.Flow = 1;
        var before = Calculate(model, manager);
        var compare = typeof(DwsimRunner).GetMethod("CheckRoundtrip", BindingFlags.NonPublic | BindingFlags.Static);
        compare.Invoke(null, new object[] { before, Calculate(model, manager) });
        Assert(true, "unchanged roundtrip accepted");
        stream.Temperature = 310;
        bool rejected = false;
        try { compare.Invoke(null, new object[] { before, Calculate(model, manager) }); }
        catch (TargetInvocationException ex) { rejected = ex.InnerException is InvalidDataException; }
        Assert(rejected, "changed roundtrip rejected");
        stream.Temperature = 300;
        stream.Name = "other-id";
        rejected = false;
        try { compare.Invoke(null, new object[] { before, Calculate(model, manager) }); }
        catch (TargetInvocationException ex) { rejected = ex.InnerException is InvalidDataException; }
        Assert(rejected, "changed inventory rejected");
        model.SimulationObjects.Clear();
        Assert(!(bool)Calculate(model, manager)["passed"], "empty model rejected");
        Console.WriteLine("Passed {0} runner gate checks (fake objects, no DWSIM runtime).", count);
        return 0;
    }
}
