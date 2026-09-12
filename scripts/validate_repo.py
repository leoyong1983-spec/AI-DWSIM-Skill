from __future__ import annotations

from pathlib import Path
import re
import sys


ROOT = Path(__file__).resolve().parents[1]
LINK_RE = re.compile(r"!?\[[^\]]+\]\(([^)]+)\)")
FRONT_MATTER_RE = re.compile(r"\A---\r?\n(.*?)\r?\n---\r?\n", re.DOTALL)
GITHUB_CLONE_URL_RE = re.compile(r"https://github\.com/[^/\s]+/[^/\s]+\.git")

REQUIRED_FILES = [
    "README.md",
    "SKILL.md",
    "AGENTS.md",
    "CONTRIBUTING.md",
    "SECURITY.md",
    "LICENSE",
    "GITHUB_REPO_SETTINGS.md",
    "agents/openai.yaml",
    "references/authority-and-path-selection.md",
    "references/basic-package-deliverables.md",
    "references/project-lessons.md",
    "references/agentic-optimization-and-mcp.md",
    "references/runtime-validation.md",
    ".github/dependabot.yml",
    ".github/pull_request_template.md",
    ".github/ISSUE_TEMPLATE/bug_report.yml",
    ".github/ISSUE_TEMPLATE/improvement.yml",
    ".github/ISSUE_TEMPLATE/config.yml",
    ".github/workflows/repo-hygiene.yml",
    "scripts/validate_repo.ps1",
    "scripts/validate_repo.py",
    "scripts/run_dwsim.ps1",
    "scripts/dwsim_runner.cs",
    "scripts/test_runner.ps1",
    "scripts/tests/runner_checks.cs",
    "scripts/tests/test_validate_repo.py",
    "scripts/tests/test_run_dwsim.py",
    "scripts/tests/launcher_fixture.cs",
]

def required_paths() -> list[str]:
    # Include future reference documents without scanning collected/private CASE assets.
    references = [path.relative_to(ROOT).as_posix() for path in (ROOT / "references").rglob("*.md")]
    return sorted(set(REQUIRED_FILES + references))


def read_text(path: Path) -> str:
    return path.read_text(encoding="utf-8")


def check_required_files(errors: list[str]) -> None:
    for relative_path in required_paths():
        path = ROOT / relative_path
        if not path.is_file():
            errors.append(f"Missing required file: {relative_path}")


def check_utf8_readability(errors: list[str]) -> dict[Path, str]:
    texts: dict[Path, str] = {}
    for relative_path in required_paths():
        path = ROOT / relative_path
        if not path.is_file():
            continue
        try:
            texts[path] = read_text(path)
        except UnicodeDecodeError as exc:
            errors.append(f"{relative_path} is not valid UTF-8: {exc}")
    return texts


def check_markdown_links(path: Path, text: str, errors: list[str]) -> None:
    for match in LINK_RE.finditer(text):
        target = match.group(1).strip()
        if not target or target.startswith("#"):
            continue
        if re.match(r"^[a-zA-Z][a-zA-Z0-9+.-]*:", target):
            continue

        relative_target = target.split("#", 1)[0].split("?", 1)[0]
        candidate = (path.parent / relative_target).resolve()

        try:
            candidate.relative_to(ROOT)
        except ValueError:
            errors.append(f"{path.relative_to(ROOT)} links outside the repository: {target}")
            continue

        if not candidate.exists():
            errors.append(f"{path.relative_to(ROOT)} contains a broken relative link: {target}")


def check_skill_front_matter(skill_text: str, errors: list[str]) -> None:
    match = FRONT_MATTER_RE.match(skill_text)
    if not match:
        errors.append("SKILL.md is missing YAML front matter.")
        return

    front_matter = match.group(1)
    for field_name in ("name:", "description:"):
        if field_name not in front_matter:
            errors.append(f"SKILL.md front matter is missing `{field_name}`.")


def check_openai_agent_yaml(agent_text: str, errors: list[str]) -> None:
    for required_key in ("interface:", "display_name:", "short_description:", "default_prompt:"):
        if required_key not in agent_text:
            errors.append(f"agents/openai.yaml is missing `{required_key}`.")


def check_readme_content(readme_text: str, errors: list[str]) -> None:
    if "<your-account>" in readme_text:
        errors.append("README.md still contains the placeholder clone URL.")
    if not GITHUB_CLONE_URL_RE.search(readme_text):
        errors.append("README.md is missing a concrete GitHub clone URL.")
    if "validate_repo.ps1" not in readme_text:
        errors.append("README.md should document the preferred PowerShell validation entry point.")
    if "validate_repo.py" not in readme_text:
        errors.append("README.md should document the repository validation command.")


def main() -> int:
    errors: list[str] = []

    check_required_files(errors)
    texts = check_utf8_readability(errors)

    for path, text in texts.items():
        if path.suffix.lower() == ".md":
            check_markdown_links(path, text, errors)

    skill_text = texts.get(ROOT / "SKILL.md")
    if skill_text is not None:
        check_skill_front_matter(skill_text, errors)

    agent_text = texts.get(ROOT / "agents/openai.yaml")
    if agent_text is not None:
        check_openai_agent_yaml(agent_text, errors)

    readme_text = texts.get(ROOT / "README.md")
    if readme_text is not None:
        check_readme_content(readme_text, errors)

    if errors:
        print("Repository validation failed:", file=sys.stderr)
        for error in errors:
            print(f"- {error}", file=sys.stderr)
        return 1

    print("Repository validation passed.")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
