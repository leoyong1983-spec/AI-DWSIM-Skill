using System;
using System.IO;
using System.Threading;

// Protocol fixture for the process launcher, not a simulator implementation.
internal static class LauncherFixture
{
    private static int Main(string[] args)
    {
        string mode = Path.GetFileName(args[0]);
        if (mode == "timeout") Thread.Sleep(30000);
        if (mode == "crash") return 7;
        string status = mode == "failed" ? "failed" : "passed";
        File.WriteAllText(Path.Combine(args[1], "run-status.json"),
            "{\"status\":\"" + status + "\",\"stage\":\"fixture\"}");
        return mode == "inconsistent" ? 7 : 0;
    }
}
