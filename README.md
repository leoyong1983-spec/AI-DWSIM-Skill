# AI-DWSIM-Skill

[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](./LICENSE)
![DWSIM](https://img.shields.io/badge/DWSIM-Automation3-blue)
![Python](https://img.shields.io/badge/Python-Orchestration-yellow)
![Package](https://img.shields.io/badge/Basic_Process_Package-Review_Ready-orange)

An AI-driven DWSIM automation and basic process package toolkit.

This repository packages a reusable skill and workflow for engineers who want to let an AI agent take over DWSIM in a controlled, auditable, script-first way.

It is designed for real execution, not just documentation. The workflow covers:

- DWSIM environment readiness checks
- `Automation3` / `.NET` takeover
- model loading or minimum viable model creation
- bounded tuning and sensitivity runs
- baseline freezing
- machine-readable exports
- review-stage basic process package generation
- release gate and blocker control

## What This Is

This repository is built around a simple idea:

1. Use official DWSIM automation capabilities as the authority layer
2. Use Python and PowerShell as orchestration layers
3. Keep every major action reproducible, reviewable, and exportable

The skill is optimized for projects such as:

- green methanol
- green hydrogen
- green ammonia
- green urea
- other process packages that need a repeatable DWSIM-to-package workflow

## Relationship to DWSIM Built-in AI Features

DWSIM now documents built-in AI capabilities such as the AI Assistant, AI Insights, AI Design Mode, AI Convergence Enhancer, and AI-assisted parameter optimization.

This repository complements those features rather than replacing them. Use DWSIM's in-app AI as an optional advisory layer for exploration, diagnosis, script drafting, or report interpretation when it is available in the target installation. Keep `Automation3`, proven project runners, and machine-readable exports as the authoritative execution and audit lane for package work.

Some DWSIM AI features are edition-specific, so this skill does not require them for the baseline workflow.

## Core Design Principles

- Script first, GUI last
- Prefer proven project runners over ad hoc clicks
- Prefer `.NET / Automation3` as the primary control lane
- Use Python as a convenience layer for orchestration, parameter injection, optimization, and batch studies
- Never overwrite a frozen baseline
- Always leave machine-auditable artifacts on disk
- Separate environment issues, model loading issues, and solver issues
- Treat release blockers and human decisions as first-class controls

## Recommended Control Stack

Preferred execution order:

1. existing proven project runner
2. direct `.NET / DWSIM.Automation.Automation3`
3. Python wrapper layer such as `dwsimopt`
4. COM only if it is actually working on the target machine
5. GUI only for layout sign-off or unavoidable visual checks

This recommendation is based on two sources:

- official DWSIM automation examples maintained in the DWSIM repository by Daniel Medeiros
- Python embedding/orchestration patterns such as `dwsimopt`

## Repository Structure

```text
AI-DWSIM-Skill/
|-- README.md
|-- SKILL.md
|-- AGENTS.md
|-- CONTRIBUTING.md
|-- SECURITY.md
|-- LICENSE
|-- GITHUB_REPO_SETTINGS.md
|-- agents/
|   `-- openai.yaml
|-- references/
|   |-- authority-and-path-selection.md
|   |-- basic-package-deliverables.md
|   `-- project-lessons.md
|-- scripts/
|   |-- validate_repo.ps1
|   `-- validate_repo.py
`-- .github/
    |-- dependabot.yml
    |-- pull_request_template.md
    |-- ISSUE_TEMPLATE/
    `-- workflows/
```

## Requirements

Minimum practical requirements:

- Windows
- DWSIM installed locally
- DWSIM Automation DLLs available
- a working `.NET` lane for `Automation3`

Optional but useful:

- Python for orchestration and batch workflows
- `pythonnet` if you want direct Python-to-DWSIM integration
- `dwsimopt` or similar wrapper if your environment matches its requirements

## Quick Start

### 1. Clone the repository

```powershell
git clone https://github.com/leoyong1983-spec/AI-DWSIM-Skill.git
cd AI-DWSIM-Skill
```

### 2. Install it as a Codex skill

Copy the runtime skill files into a Codex skill folder named `ai-dwsim-basic-package`.

```powershell
$source = Get-Location
$target = "$env:USERPROFILE\\.codex\\skills\\ai-dwsim-basic-package"
New-Item -ItemType Directory -Force $target | Out-Null
Copy-Item `
  "$source\\README.md", `
  "$source\\SKILL.md", `
  "$source\\agents", `
  "$source\\references" `
  -Destination $target -Recurse -Force
```

Keep the full repository clone around if you also want the repository maintenance scaffolding such as `AGENTS.md`, local validation scripts, or GitHub workflow files.

### 3. Use it in a real DWSIM task

Example prompts:

```text
Use ai-dwsim-basic-package to verify the DWSIM environment, run an Automation3 smoke test, and report the working control lane.
```

```text
Use ai-dwsim-basic-package to load the latest valid workcopy, calculate the flowsheet, and export key streams, equipment, utility summary, status, and open issues.
```

```text
Use ai-dwsim-basic-package to freeze the accepted case as a review-stage baseline and prepare machine-readable package outputs.
```

### Option A: Use it as a Codex skill

Copy this repository into your Codex skills directory under the folder name `ai-dwsim-basic-package`.

Typical local path:

```powershell
$skillPath = "$env:USERPROFILE\\.codex\\skills\\ai-dwsim-basic-package"
```

Then invoke it in a task such as:

- `Use ai-dwsim-basic-package to take over DWSIM, run a smoke test, and export key streams and equipment.`
- `Use ai-dwsim-basic-package to freeze the current workcopy as a review-stage baseline and generate package exports.`
- `Use ai-dwsim-basic-package to run bounded throughput tuning without changing topology.`

### Option B: Use it as a workflow reference in your own repository

If you do not use Codex skills directly, you can still reuse:

- `SKILL.md` as the operating playbook
- `AGENTS.md` as the repository-specific maintenance contract for AI coding agents
- `references/authority-and-path-selection.md` to choose the correct control lane
- `references/project-lessons.md` to avoid known failure modes
- `references/basic-package-deliverables.md` to structure exports and review-stage package outputs

## Maintenance and Validation

This repository includes lightweight open-source maintenance scaffolding:

- `AGENTS.md` for repository-specific AI agent instructions
- `CONTRIBUTING.md` for contribution scope and review expectations
- `SECURITY.md` for vulnerability reporting guidance
- `.github/ISSUE_TEMPLATE/` and `.github/pull_request_template.md` for consistent collaboration
- `.github/workflows/repo-hygiene.yml` for push, pull request, manual, and daily repository checks
- `scripts/validate_repo.ps1` as the Windows-friendly local validation entry point
- `scripts/validate_repo.py` for the underlying repository smoke checks without requiring DWSIM

Run the local validation entry point after repository-facing changes:

```powershell
.\scripts\validate_repo.ps1
```

The PowerShell wrapper prefers a real Python installation behind `py` or `python` and fails with a clear message if neither is available.

## Typical Workflow

1. Verify the environment
2. Prove the control lane with a smoke test
3. Reuse an existing valid model if possible
4. Build a minimum viable model only if all candidate models fail
5. Run calculations and classify failures correctly
6. Perform bounded tuning only when explicitly allowed
7. Freeze the accepted case as a baseline
8. Export machine-readable results
9. Compile review-stage package deliverables
10. Run release-gate checks before issue

## What Good Output Looks Like

A good run should leave artifacts such as:

- `headless_results.json`
- `key_streams.csv`
- `key_equipment.csv`
- `utility_summary.csv`
- assumptions / open issues / run status notes
- baseline or workcopy traceability
- review-stage package outputs when requested

## What This Toolkit Does Not Do By Default

It does not assume:

- detailed design is complete
- a frozen baseline may be overwritten
- free tuning remains allowed after the project enters review-stage package work
- human decision items may be closed automatically by AI

## References

Official DWSIM automation source:

- [DanWBR/dwsim](https://github.com/DanWBR/dwsim)

Example automation files:

- [newAPI.cs](https://raw.githubusercontent.com/DanWBR/dwsim/windows/DWSIM.Automation.Tests.CSharp/newAPI.cs)
- [looptest.cs](https://raw.githubusercontent.com/DanWBR/dwsim/windows/DWSIM.Automation.Tests.CSharp/looptest.cs)
- [Module1.vb](https://raw.githubusercontent.com/DanWBR/dwsim/windows/DWSIM.Automation.Tests/Module1.vb)

Python wrapper / orchestration example:

- [lf-santos/dwsimopt](https://github.com/lf-santos/dwsimopt)

Related AI process simulation work:

- [Context is all you need: Towards autonomous model-based process design using agentic AI in flowsheet simulations](https://arxiv.org/abs/2603.12813)
- [From Text to Simulation: A Multi-Agent LLM Workflow for Automated Chemical Process Design](https://doi.org/10.1609/aaai.v40i35.40215)
- [Sketch2Simulation: Automating Flowsheet Generation via Multi Agent Large Language Models](https://arxiv.org/abs/2603.24629)
- [AutoChemSchematic AI: Agentic Physics-Aware Automation for Chemical Manufacturing Scale-Up](https://arxiv.org/abs/2505.24584)
- [Improving process systems engineering with specialized multi-agent large language models](https://doi.org/10.1016/j.ceja.2026.101141)
- [Flowsheet Copilot](https://simulate365.com/landing-pages/copilot/)

## Publishing Note

This repository now ships with the MIT license.

If you fork or republish this repository, copy the values from [GITHUB_REPO_SETTINGS.md](GITHUB_REPO_SETTINGS.md) into the repository "About" section.
