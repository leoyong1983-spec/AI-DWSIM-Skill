# 2026-04-30 - DWSIM 10 Patreon Edition and Automation API Check

## Sources

- DWSIM downloads: https://dwsim.org/index.php/download/
- DWSIM Automation3 API: https://dwsim.org/api_help/html/T_DWSIM_Automation_Automation3.htm
- DWSIM Automation3 ReleaseResources API: https://dwsim.org/api_help/html/M_DWSIM_Automation_Automation3_ReleaseResources.htm
- DWSIM GitHub releases: https://github.com/DanWBR/dwsim/releases

## Summary

The official download page listed the latest open-source release as `v9.0.5` during the original check. On 2026-05-16, the same page listed the latest Patreon Edition release as `v10.0.9630.31147`, published on 2026-05-13.

The public Automation3 API documentation is now shown as `DWSIM.Automation.dll` version `10.0.0.0`. It still exposes the methods this project treats as the authoritative automation lane, including `CreateFlowsheet`, `LoadFlowsheet`, `CalculateFlowsheet2`, `SaveFlowsheet`, `SaveFlowsheet2`, `GetVersion`, and `ReleaseResources`.

The GitHub releases page still marks `v9.0.5` as the latest open-source release.

## Relevance to AI-DWSIM-Skill

This does not require a change to the control-lane strategy. `Automation3` remains the correct default authority layer for auditable external automation.

It does reinforce two maintenance requirements:

1. Runners should discover the installed DWSIM version and `DWSIM.Automation.dll` path at runtime instead of hard-coding one major version.
2. Batch automation and crash-hunting runs must call `ReleaseResources()` in cleanup paths to avoid retaining flowsheets or automation managers after success or failure.

## Recommended Project Action

Keep README version-agnostic and document the lifecycle contract in `SKILL.md` and the authority reference.

For future code runners, log `GetVersion()` and the resolved automation assembly path during readiness checks. Treat DWSIM 10 Patreon Edition as a compatibility signal, not as a new baseline dependency, because the open-source release remains `v9.0.5`.

## Update Warranted

Documentation update warranted. No DWSIM runtime code was present in this repository during the check.
