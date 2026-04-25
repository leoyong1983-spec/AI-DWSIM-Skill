# 2026-04-25 - DWSIM Official AI Capabilities

## Sources

- DWSIM homepage: https://dwsim.org/
- DWSIM Automation namespace: https://dwsim.org/api_help/html/N_DWSIM_Automation.htm
- DWSIM releases: https://github.com/DanWBR/dwsim/releases

## Summary

The official DWSIM homepage now presents AI as part of the simulator workflow. It describes an integrated AI Assistant, AI Insights for flowsheets and unit operations, AI Design Mode, an AI Convergence Enhancer, AI-assisted parameter optimization, and AI insights for LCA and TEA reporting.

The same homepage still highlights programmatic integration paths that matter to this project: Python, .NET, COM Automation API, and a TCP/IP server for headless simulation backends. The DWSIM API documentation continues to expose the `DWSIM.Automation` namespace, including `Automation3`.

The latest GitHub release visible during this check is `v9.0.5`, which is mainly a bug-fix release and does not change this project's Automation3-first control-lane recommendation.

## Relevance to AI-DWSIM-Skill

This is highly relevant because AI-DWSIM-Skill should not present itself as the only AI layer around DWSIM. DWSIM itself now has in-app AI capabilities, while this repository is better positioned as the external, auditable, script-first control and package workflow.

The built-in AI features are useful as optional advisory tools, but they should not replace the validated execution chain:

- environment readiness checks
- Automation3 or proven runner smoke tests
- load/calculate/save verification
- machine-readable exports
- baseline traceability
- release blocker handling

Some AI features are described as Patreon Edition exclusive, so this project should not depend on them for its baseline workflow.

## Recommended Project Action

Add a README note clarifying that AI-DWSIM-Skill complements DWSIM's built-in AI capabilities. The note should explain that built-in AI outputs can help with exploration, diagnosis, and script drafting, while Automation3 and project-local runners remain the authoritative execution path for auditable package work.

## Update Warranted

Documentation update warranted. No code change is needed.
