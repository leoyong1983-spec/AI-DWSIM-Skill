# Agentic Optimization and MCP Boundaries

Use this reference when new AI-for-science, text-to-flowsheet, MCP, or simulator-agent material suggests adding optimization or tool-server behavior to a DWSIM workflow.

The project may learn from these signals, but the baseline does not change: DWSIM-native calculation through `Automation3`, a proven local runner, or another locally validated control lane remains the authority layer.

## Text-to-Flowsheet Intake

Treat natural-language or diagram-derived flowsheets as candidate inputs, not validated models.

A text-to-flowsheet path must produce:

1. a candidate topology or graph representation;
2. explicit assumptions for missing compounds, units, operating conditions, and property packages;
3. a loadable DWSIM model or minimum viable model;
4. a calculation run through the validated control lane;
5. machine-readable solver status, stream/equipment exports, and open assumptions.

Do not accept an LLM-generated topology, diagram, or narrative as validation evidence by itself.

## Bounded Numerical Optimization

Use numerical optimization only after a model can be loaded, changed, calculated, and exported through a proven runner.

Before any optimizer-driven run, define:

1. the exact adjustable variable family;
2. lower and upper bounds;
3. the objective or residual exported by DWSIM;
4. a maximum evaluation count;
5. the rollback workcopy;
6. stop conditions for solver failure, invalid physics, or reviewer-stage lock.

The optimizer may help search local parameters such as recycle ratio, split fraction, purge fraction, or utility set point. It must not silently change property package, reaction route, topology, unit-operation identity, or frozen baseline settings.

Do not add `scipy`, surrogate models, or other optimization dependencies to the skill by default. If a project-local runner already has a compatible optimizer, use it as a convenience layer and log the dependency, version, bounds, and evaluation history.

Always re-run and export the accepted result through DWSIM-native calculation before reporting a tuned case as valid.

## MCP Tool Boundary

MCP-style tools can be useful as a standard interface for simulator inspection, calculation, and export, but they are not automatically authoritative.

An MCP DWSIM tool lane should be treated as experimental until it records:

1. DWSIM edition and version;
2. automation assembly or server version;
3. supported tool schema and whether each tool is read-only or mutating;
4. local installation and launch steps;
5. smoke-test command and result;
6. model lock or queue behavior;
7. exported evidence paths.

Prefer read-only MCP tools first: inspect flowsheet, list objects, read stream properties, and export status. Mutating tools must operate on a workcopy and must run calculation plus exports before reporting success.

Do not add an MCP server as an install-time dependency for this repository unless its license, version support, Windows behavior, and smoke tests are reviewed in a separate branch.

## COM and GUI Constraints

If only COM or GUI control is available, treat it as weaker than direct `.NET` control.

For COM-based simulator automation:

1. run one simulator instance per lock;
2. serialize mutating calls;
3. use explicit timeouts;
4. capture crashes or hangs separately from solver non-convergence;
5. save and reload workcopies when possible;
6. release resources and log cleanup.

GUI actions are supporting evidence only. They do not replace machine-readable calculation outputs.

## Reporting Rule

When using text-to-flowsheet, optimizer, MCP, COM, or GUI-assisted behavior, report the control lane, assumptions, bounds, solver status, exported evidence, and remaining risks. Do not describe the result as validated unless DWSIM calculated it successfully under the recorded lane.
