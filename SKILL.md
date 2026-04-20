---
name: ai-dwsim-basic-package
description: "Control DWSIM with auditable, script-first workflows to create, update, tune, freeze, and export a basic process package. Use when Codex must take over DWSIM through Automation3/.NET, Python wrappers, or proven project runners to: (1) verify the environment, (2) load or build a flowsheet, (3) run calculations, (4) perform bounded sensitivity or tuning, (5) export machine-readable results, or (6) compile review-stage basic process package deliverables without changing frozen calculation boundaries."
---

# AI DWSIM Basic Package

## Overview

Use this skill when the task is not theoretical process discussion but actual DWSIM execution with reproducible artifacts.

Prefer scriptable, reviewable, repeatable control lanes:

1. Existing proven project runner
2. Direct `DWSIM.Automation.Automation3` or project C# runner
3. Python wrapper layer such as `dwsimopt` when the environment matches its requirements
4. COM only if already registered and working
5. GUI only for layout sign-off or unavoidable visual checks

Read [references/authority-and-path-selection.md](references/authority-and-path-selection.md) before choosing the control lane.

Read [references/project-lessons.md](references/project-lessons.md) when resuming an existing DWSIM project or when a baseline/review/release workflow already exists.

Read [references/basic-package-deliverables.md](references/basic-package-deliverables.md) before generating package outputs.

## Workflow

### 1. Gate the execution path

Decide the path in this order:

1. If the workspace already contains proven DWSIM runners, smoke tests, tuning scripts, or export tools, reuse them first.
2. If direct `.NET` loading works, use `Automation3` as the default execution lane.
3. If a Python wrapper is explicitly available and compatible, use it as a convenience layer for variable injection, optimization loops, or result exchange.
4. If only COM works, use COM carefully and log that the lane is legacy or weaker than direct `.NET`.
5. Do not default to GUI clicking for production work.

Treat `Automation3` as the authoritative baseline lane because it exposes `CreateFlowsheet`, `LoadFlowsheet`, `CalculateFlowsheet2`, and `SaveFlowsheet` directly.

Treat `dwsimopt`-style wrappers as an accelerator layer, not as the primary truth source, because they can simplify Python control but also add environment constraints such as Python version and `pythonnet` compatibility.

### 2. Verify the environment before touching the model

Always check:

1. DWSIM installation path
2. DWSIM version
3. Automation DLL presence
4. Whether direct `.NET` loading works
5. Whether COM is registered
6. Whether existing model files, workcopies, audits, status files, and package exports already exist

Do not begin tuning or package compilation before confirming which control lane actually works.

If multiple lanes work, prefer the one already proven in the current workspace.

### 3. Choose the model with strict priority

Use this priority order:

1. Frozen formal baseline if the task is review support, package compilation, or evidence extraction
2. Latest loadable audited workcopy if the task is bounded tuning or pre-freeze improvement
3. Latest loadable mother model if no frozen or tuned workcopy is available
4. Minimum once-through model only when no usable model loads

Never overwrite a frozen baseline.

If a frozen baseline exists, copy it to a new workcopy before any executable change.

### 4. Run with bounded intent

Use explicit run modes:

1. `readiness` for environment and smoke tests
2. `load-and-calculate` for proving the model can run
3. `bounded-tuning` for small, auditable parameter changes
4. `freeze-and-export` for baseline locking and package generation
5. `review-support` for comment closure, consistency checking, and supplemental outputs

For bounded tuning:

1. Freeze property package, reaction route, topology, key equipment naming, and already-proven convergence structure unless the user explicitly reopens them.
2. Change one logical variable family at a time.
3. Record old value, new value, convergence state, engineering comment, and effect on key KPIs.
4. Prefer the minimum change that clears the target.
5. Stop if the task has moved into review or release support mode.

### 5. Export machine-readable outputs first

Prefer `CSV`, `JSON`, and concise `Markdown` summaries before `Word`, `Excel`, or `PowerPoint`.

At minimum, export:

1. Key streams
2. Key equipment
3. Solver status
4. Assumptions and boundary notes
5. Error capture
6. Traceability to the source workcopy

When the project is already in package/review mode, treat the machine-readable layer as the source of truth for formal Office deliverables.

### 6. Enforce release discipline

If any mismatch appears between:

1. Frozen model object inventory
2. Exported result tables
3. Package narrative or equipment list

create a release blocker immediately and switch to blocker-resolution mode.

Do not describe the package as clean for release while a release blocker is still open.

Human decisions stay open until explicitly closed by the user or another authorized human reviewer.

### 7. Keep the package at the right depth

This skill is for review-stage basic process package work, not detailed design by default.

Do not silently upgrade the project claim from:

- review-stage basic process package

to:

- detailed design

unless the user explicitly changes the project stage and depth.

Detailed sizing, full control philosophy, full safeguards/interlocks, and full datasheets belong to a later phase unless the project explicitly says otherwise.

### 8. Chinese-first delivery rule

If the project requires Chinese submission:

1. Use Chinese for reader-facing file names and正文.
2. Keep necessary English only for paths, file extensions, stream tags, equipment tags, API names, object names, or standard abbreviations.
3. Add Chinese annotations for reader-visible English terms that remain.
4. Do not mistake console mojibake for real file corruption; verify with UTF-8 reads or formal extraction tools before editing.

## Guardrails

- Do not claim a model solved if it did not.
- Do not mix load failure, object-binding failure, and solver failure.
- Do not replace auditable script control with GUI-only actions.
- Do not overwrite a frozen baseline.
- Do not reopen free tuning after the project enters package review unless the user explicitly authorizes it.
- Do not close human decisions without explicit human direction.

## Source hierarchy

Use external knowledge in this order:

1. Official DWSIM repository and author-maintained automation examples
2. Official or repository-level documentation for the Python wrapper
3. Proven project-local runners and prior validated logs
4. Secondary community material only as fallback

## Output expectations

When this skill is used well, the result should be a chain of artifacts, not just advice:

1. Workcopy or frozen baseline reference
2. Logs showing what lane was used and why
3. Calculation or tuning outputs
4. Export tables
5. Package-stage formal deliverables or review-support files when requested
