# 2026-05-17 - OpenAI Codex Windows Sandbox and Supply-Chain Safety

## Sources

- OpenAI: Building the Codex Windows sandbox: https://openai.com/index/building-codex-windows-sandbox/
- OpenAI: Our response to the TanStack npm supply chain attack: https://openai.com/index/our-response-to-the-tanstack-npm-supply-chain-attack/

## Summary

OpenAI's Windows sandbox article explains the engineering constraints behind running Codex safely on Windows, including isolation boundaries, file-system access control, and command execution constraints. This is directly relevant to AI-DWSIM-Skill because the project targets Windows-based DWSIM automation and relies on local scripts, Git operations, and external simulator binaries.

OpenAI's TanStack supply-chain response is a separate but related maintenance signal. It reinforces that agentic development environments must treat package installers, generated scripts, and dependency updates as supply-chain risk surfaces, even when the repository itself is small and lightweight.

## Relevance to AI-DWSIM-Skill

This signal supports the current maintenance posture:

- keep repository validation lightweight and auditable
- prefer explicit branches and reviewable diffs
- avoid broad filesystem writes outside the intended workspace
- do not add runtime dependencies unless they directly improve DWSIM control or validation
- report Git/network/credential blockers instead of hiding them
- keep DWSIM runtime claims separate from repository-only validation

It is especially relevant to Windows local validation because DWSIM Automation3 work often depends on machine-local DLL paths, simulator installations, and permissions that should be discovered and logged rather than assumed.

## Recommended Project Action

No code change is needed. Continue using the existing Windows validation entry point and keep automation scripts dependency-light.

For future DWSIM runner work, keep sandbox assumptions explicit: log the working directory, resolved Automation DLL path, DWSIM version, and any required permissions. Avoid installing packages or executing downloaded scripts inside heartbeat maintenance unless the change is reviewed and directly needed.

## Update Warranted

CASE note warranted. Code change not warranted.
