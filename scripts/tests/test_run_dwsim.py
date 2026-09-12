"""Exercise Windows PowerShell 5.1 launcher behavior without installing DWSIM."""
import json
import os
from pathlib import Path
import shutil
import subprocess
import tempfile
import unittest


ROOT = Path(__file__).resolve().parents[2]


@unittest.skipUnless(os.name == "nt", "Windows process launcher only")
class LauncherChecks(unittest.TestCase):
    def setUp(self):
        output = ROOT / "AGENTCODING/05_qa"
        output.mkdir(parents=True, exist_ok=True)
        self.temp = tempfile.TemporaryDirectory(prefix="launcher spaces ", dir=output)
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name)
        shutil.copyfile(ROOT / "scripts/run_dwsim.ps1", self.root / "run_dwsim.ps1")
        shutil.copyfile(ROOT / "scripts/tests/launcher_fixture.cs", self.root / "dwsim_runner.cs")

    def run_fixture(self, mode, seconds=10):
        install = self.root / mode
        install.mkdir(exist_ok=True)
        (install / "DWSIM.Automation.dll").touch()
        output = self.root / "results"
        run = subprocess.run([
            "powershell.exe", "-NoProfile", "-ExecutionPolicy", "Bypass", "-File",
            str(self.root / "run_dwsim.ps1"), "-DwsimDirectory", str(install),
            "-OutputDirectory", str(output), "-TimeoutSeconds", str(seconds),
        ], capture_output=True, timeout=20)
        report = json.loads((output / "run-status.json").read_text(encoding="utf-8-sig"))
        return run.returncode, report

    def test_success_exit_code_on_windows_powershell(self):
        code, report = self.run_fixture("ok")
        self.assertEqual(code, 0)
        self.assertEqual(report["status"], "passed")

    def test_failed_evidence_even_with_zero_exit(self):
        code, report = self.run_fixture("failed")
        self.assertNotEqual(code, 0)
        self.assertEqual(report["status"], "failed")

    def test_nonzero_exit_cannot_keep_success_report(self):
        code, report = self.run_fixture("inconsistent")
        self.assertNotEqual(code, 0)
        self.assertEqual(report["status"], "failed")
        self.assertTrue((self.root / "results/worker-status.json").is_file())

    def test_crash_without_report(self):
        code, report = self.run_fixture("crash")
        self.assertNotEqual(code, 0)
        self.assertEqual(report["stage"], "worker")
        self.assertEqual(report["status"], "failed")

    def test_timeout(self):
        code, report = self.run_fixture("timeout", seconds=1)
        self.assertNotEqual(code, 0)
        self.assertEqual(report["stage"], "timeout")
        self.assertEqual(report["status"], "failed")

    def test_existing_evidence_is_preserved(self):
        self.run_fixture("ok")
        path = self.root / "results/run-status.json"
        original = path.read_bytes()
        code, _ = self.run_fixture("ok")
        self.assertNotEqual(code, 0)
        self.assertEqual(path.read_bytes(), original)


if __name__ == "__main__":
    unittest.main()
