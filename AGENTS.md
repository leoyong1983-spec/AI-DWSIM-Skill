# AGENTS.md

## Mission

Keep this repository installable, reviewable, and trustworthy as an AI-ready DWSIM skill. Favor small, high-confidence improvements over speculative expansion.

## Repository Map

- `SKILL.md` is the primary skill contract.
- `references/` contains the domain guardrails that explain control-lane choice, package deliverables, and project lessons.
- `agents/openai.yaml` contains the Codex-facing UI metadata.
- `scripts/validate_repo.ps1` is the preferred local validation entry point on Windows.
- `scripts/validate_repo.py` is the lightweight repository smoke test.
- `.github/` contains collaboration, dependency, and CI hygiene for the open-source repo.

## Source Hierarchy

Use external knowledge in this order:

1. Official DWSIM repository and author-maintained automation examples
2. Repository-level documentation for wrapper layers such as `dwsimopt`
3. Proven project-local runners, logs, and validated workcopies
4. Secondary community material only as fallback

## Maintenance Priorities

1. Fix install failures, broken links, README-to-repo drift, missing validation, and open-source hygiene gaps first.
2. Prefer low-risk improvements that can be reviewed in a small branch or PR.
3. Keep the repository lightweight; do not add large dependencies or speculative frameworks.
4. Preserve the current project positioning as an auditable, script-first DWSIM skill for review-stage basic process packages.
5. Treat Chinese reader-facing content carefully and verify encoding before editing.

## Guardrails

- Do not change the license without maintainer approval.
- Do not commit secrets, credentials, proprietary plant data, private model files, or personal machine paths.
- Do not claim DWSIM runtime validation unless a real local DWSIM environment was available and exercised.
- Do not silently upgrade the scope from basic process package support to detailed design.
- Do not replace official DWSIM automation authority with speculative wrapper behavior.

## Validation

After repository-facing changes, run:

```powershell
.\scripts\validate_repo.ps1
```

If you add or change GitHub workflows, keep them dependency-light and explain any schedule or timezone assumptions in the workflow or PR description.

## Safe Change Areas

These are normally safe to improve without extra approval:

- documentation consistency
- installation instructions
- issue and pull request templates
- SECURITY / CONTRIBUTING guidance
- lightweight GitHub Actions and Dependabot configuration
- repository smoke tests that do not require DWSIM binaries

Use an issue or maintainer note instead of direct edits for major roadmap changes, architectural repositioning, or large dependency additions.
