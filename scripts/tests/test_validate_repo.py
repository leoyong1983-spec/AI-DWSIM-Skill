"""Regression checks for missing runtime files and unvalidated references."""
import contextlib
import importlib.util
import io
from pathlib import Path
import shutil
import tempfile
import unittest
from unittest.mock import patch


ROOT = Path(__file__).resolve().parents[2]
spec = importlib.util.spec_from_file_location("validate_repo", ROOT / "scripts/validate_repo.py")
validator = importlib.util.module_from_spec(spec)
spec.loader.exec_module(validator)


class RepositoryChecks(unittest.TestCase):
    def setUp(self):
        output = ROOT / "AGENTCODING/05_qa"
        output.mkdir(parents=True, exist_ok=True)
        self.temp = tempfile.TemporaryDirectory(dir=output)
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name)
        for relative in validator.required_paths():
            target = self.root / relative
            target.parent.mkdir(parents=True, exist_ok=True)
            shutil.copyfile(ROOT / relative, target)

    def check(self):
        messages = io.StringIO()
        with patch.object(validator, "ROOT", self.root), contextlib.redirect_stderr(messages), contextlib.redirect_stdout(messages):
            result = validator.main()
        return result, messages.getvalue()

    def test_current_package(self):
        self.assertEqual(self.check()[0], 0)

    def test_runtime_source_required(self):
        (self.root / "scripts/dwsim_runner.cs").unlink()
        code, message = self.check()
        self.assertEqual(code, 1)
        self.assertIn("Missing required file: scripts/dwsim_runner.cs", message)

    def test_directory_is_not_a_required_file(self):
        path = self.root / "scripts/run_dwsim.ps1"
        path.unlink()
        path.mkdir()
        self.assertEqual(self.check()[0], 1)

    def test_new_reference_links_are_checked(self):
        (self.root / "references/new-note.md").write_text("[missing](absent.md)", encoding="utf-8")
        code, message = self.check()
        self.assertEqual(code, 1)
        self.assertIn("absent.md", message)

    def test_new_reference_encoding_is_checked(self):
        (self.root / "references/new-note.md").write_bytes(b"\xff")
        code, message = self.check()
        self.assertEqual(code, 1)
        self.assertIn("not valid UTF-8", message)


if __name__ == "__main__":
    unittest.main()
