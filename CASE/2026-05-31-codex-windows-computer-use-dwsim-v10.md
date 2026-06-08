# 2026-05-31 - Codex Windows Computer Use and DWSIM 10 Compatibility Signal

## Sources

- OpenAI Codex changelog: https://developers.openai.com/codex/changelog/
- OpenAI Codex computer-use documentation: https://developers.openai.com/codex/app/computer-use
- DWSIM downloads: https://dwsim.org/index.php/download/

## Summary

OpenAI's Codex changelog now lists Windows desktop computer-use support, letting Codex operate local GUI applications through a controlled computer-use flow. This is relevant to DWSIM because simulator availability, DLL paths, and manual validation evidence often depend on Windows-local installations that cannot be reproduced by repository-only CI.

The official DWSIM download page also lists Patreon Edition `v10.0.9644.28845`, published on 2026-05-28. This continues the DWSIM 10 compatibility trend already tracked by this project, but it does not change the public baseline requirement: AI-DWSIM-Skill should not depend on Patreon-only features for installation or repository smoke checks.

## Relevance to AI-DWSIM-Skill

This is a high-relevance maintenance signal for Windows-local validation and simulator-facing evidence collection. Computer use may help a maintainer inspect a real DWSIM installation, launch the UI, capture screenshots, or verify manual setup paths when script-level checks are insufficient.

It should not replace the repository's script-first control policy. Automation3, proven local runners, and machine-readable exports remain the authority lanes for repeatable process-simulation work. GUI operation should be treated as supplemental evidence only unless a future workflow records exact inputs, outputs, and simulator version metadata.

## Recommended Project Action

Keep the current Windows validation entry point and DWSIM control-lane boundaries unchanged.

For a future branch, consider a documentation-only checklist for optional GUI-assisted DWSIM validation:

- record DWSIM edition and version;
- record installation and resolved automation DLL paths;
- capture screenshots only as supporting evidence;
- require machine-readable exports before claiming simulation validation;
- avoid storing private model files, plant data, or local personal paths.

## Update Warranted

CASE note warranted. README, SKILL, dependency, or workflow changes are not warranted today.
