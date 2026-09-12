# AI-DWSIM-Skill

[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](./LICENSE)
![DWSIM](https://img.shields.io/badge/DWSIM-Automation3-blue)
![Python](https://img.shields.io/badge/Python-Orchestration-yellow)
![Package](https://img.shields.io/badge/Basic_Process_Package-Review_Ready-orange)

An AI-driven DWSIM automation and basic process package toolkit.

面向真实 DWSIM 执行的技能：优先使用已有模型；无模型时建立最小模型，并留下可复核的计算证据。仓库现提供可运行的水混合器示例及已有模型重算入口，详见[中英双语使用说明](references/runtime-validation.md)。

An executable water-mixer example and an existing-model recalculation entry point are included. See the [bilingual runtime guide](references/runtime-validation.md) for commands and validation boundaries.

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

Recent DWSIM documentation also lists the DWSIM MCP Server and Fluent API as official automation-facing features. Treat these as promising integration lanes to evaluate, not as a replacement for the current verified baseline.

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
- Provide Chinese-English bilingual explanations for reader-facing guidance, validation summaries, and handoff notes

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
|   |-- agentic-optimization-and-mcp.md
|   |-- basic-package-deliverables.md
|   |-- runtime-validation.md
|   `-- project-lessons.md
|-- scripts/
|   |-- run_dwsim.ps1
|   |-- dwsim_runner.cs
|   |-- test_runner.ps1
|   |-- tests/
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

Copy the runtime skill files into a Codex skill folder named `ai-dwsim-basic-package`. Include both runner files so the installed skill can execute the documented commands.

安装时同时复制下面两个执行文件，避免技能装好后缺少运行入口。

```powershell
$source = Get-Location
$target = "$env:USERPROFILE\\.codex\\skills\\ai-dwsim-basic-package"
New-Item -ItemType Directory -Force $target | Out-Null
Copy-Item `
  "$source\\README.md", `
  "$source\\LICENSE", `
  "$source\\SKILL.md", `
  "$source\\agents", `
  "$source\\references" `
  -Destination $target -Recurse -Force
New-Item -ItemType Directory -Force "$target\\scripts" | Out-Null
Copy-Item "$source\\scripts\\run_dwsim.ps1", "$source\\scripts\\dwsim_runner.cs" `
  -Destination "$target\\scripts" -Force
```

Keep the full repository clone for repository maintenance: `AGENTS.md`, validation/test scripts, and GitHub workflows. Run repository validation in that clone, not in the minimal installed skill.

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

Use the runtime-only copy command in step 2 above. Keep collected CASE data and local work artifacts outside the installed skill.

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
- `references/agentic-optimization-and-mcp.md` to keep text-to-flowsheet, optimizer, MCP, COM, and GUI-assisted workflows bounded
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

The PowerShell wrapper prefers `py -3.12`, warns when falling back to another available Python, and fails with a clear message if no usable interpreter is available.

Run the regression checks from the full clone:

```powershell
py -3.12 -B -m unittest discover -s scripts/tests -p "test_*.py" -v
.\scripts\test_runner.ps1 -OutputDirectory '.\AGENTCODING\05_qa\runner-gates-01'
```

仓库检查与 CI 不需要 DWSIM；`test_runner.ps1` 使用测试替身验证失败判定。真实模拟需执行[运行指南](references/runtime-validation.md)中的 `run_dwsim.ps1`，两种验证不可混淆。

Repository checks and CI do not require DWSIM. `test_runner.ps1` uses fake objects to exercise failure gates; actual simulation evidence comes from `run_dwsim.ps1` as documented in the [runtime guide](references/runtime-validation.md).

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

## Bilingual Explanation Requirement

Reader-facing explanations should be bilingual by default:

- Chinese first, for engineering review and Chinese project delivery.
- English second, for GitHub readers and international collaborators.
- Paths, commands, stream tags, equipment tags, API names, JSON keys, and CSV headers should stay unchanged unless a task explicitly asks for localization.
- Machine-readable outputs do not need every row duplicated in two languages; add bilingual notes, summaries, and assumptions around them.

## Agentic Optimization and MCP Boundary

Recent text-to-flowsheet and MCP simulator-agent work is relevant, but it does not replace DWSIM-native validation.

For this repository:

- Text-to-flowsheet outputs are candidate topologies until a DWSIM model loads, calculates, and exports evidence.
- Local numerical optimizers may be used only inside bounded tuning with explicit variables, bounds, objective or residual source, maximum evaluations, rollback, and a final DWSIM-native re-run.
- MCP tools are optional interface adapters, not install-time dependencies or proof of validation.
- COM and GUI control remain fallback/support lanes and must not replace machine-readable solver status and exports.

See [references/agentic-optimization-and-mcp.md](references/agentic-optimization-and-mcp.md) for the detailed guardrails.

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
- [DWSIM Tutorials](https://dwsim.org/tutorials/en/)
- [DWSIM AI Assistant tutorial](https://dwsim.org/tutorials/en/features/ai-assistant.html)

Example automation files:

- [newAPI.cs](https://raw.githubusercontent.com/DanWBR/dwsim/windows/DWSIM.Automation.Tests.CSharp/newAPI.cs)
- [looptest.cs](https://raw.githubusercontent.com/DanWBR/dwsim/windows/DWSIM.Automation.Tests.CSharp/looptest.cs)
- [Module1.vb](https://raw.githubusercontent.com/DanWBR/dwsim/windows/DWSIM.Automation.Tests/Module1.vb)

Python wrapper / orchestration example:

- [lf-santos/dwsimopt](https://github.com/lf-santos/dwsimopt)

Related AI process simulation work:

- [Context is all you need: Towards autonomous model-based process design using agentic AI in flowsheet simulations](https://arxiv.org/abs/2603.12813)
- [From Text to Simulation: A Multi-Agent LLM Workflow for Automated Chemical Process Design](https://doi.org/10.1609/aaai.v40i35.40215)
- [Text-to-Flowsheet: expert-level digitization and automated simulation of chemical processes from natural language](https://doi.org/10.1039/D6DD00060F)
- [Sketch2Simulation: Automating Flowsheet Generation via Multi Agent Large Language Models](https://arxiv.org/abs/2603.24629)
- [AutoChemSchematic AI: Agentic Physics-Aware Automation for Chemical Manufacturing Scale-Up](https://arxiv.org/abs/2505.24584)
- [Improving process systems engineering with specialized multi-agent large language models](https://doi.org/10.1016/j.ceja.2026.101141)
- [Large Language Models in Process Systems Engineering: Opportunities, Architectures, and Industrial Deployment Challenges](https://arxiv.org/abs/2606.11589)
- [Large Language Model Agent for User-friendly Chemical Process Simulations](https://arxiv.org/abs/2601.11650)
- [Flowsheet Copilot](https://simulate365.com/landing-pages/copilot/)

## Publishing Note

This repository now ships with the MIT license.

If you fork or republish this repository, copy the values from [GITHUB_REPO_SETTINGS.md](GITHUB_REPO_SETTINGS.md) into the repository "About" section.
