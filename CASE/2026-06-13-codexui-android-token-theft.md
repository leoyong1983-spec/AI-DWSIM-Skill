# 2026-06-13 - Malicious codexui-android Package Targeting Codex Tokens

## Sources

- Aikido Security: Malicious `codexui-android` NPM package steals OpenAI Codex credentials: https://www.aikido.dev/blog/malicious-codexui-android-npm-package-steals-openai-codex-credentials
- Snyk vulnerability database: `codexui-android`: https://security.snyk.io/package/npm/codexui-android

## Summary

Security researchers reported a malicious NPM package named `codexui-android` that impersonated a Codex-related Android utility and exfiltrated Codex OAuth credentials, including refresh tokens. Snyk's advisory identifies the package as malicious and recommends removing it.

The package is not part of this repository and is not required by AI-DWSIM-Skill. The signal is still relevant because this project is maintained through agent-assisted workflows, and compromised local agent credentials could affect pull requests, repository settings, or future automation.

## Relevance to AI-DWSIM-Skill

This is an agent maintenance and supply-chain safety signal, not a DWSIM runtime signal. It reinforces the existing project rule: keep the skill lightweight, avoid unreviewed helper packages, and do not install unofficial Codex-branded packages as part of repository validation or automation.

It also supports keeping DWSIM execution scripts dependency-light and explicit. Repository smoke tests should continue to use only PowerShell, Python standard-library checks, and GitHub-hosted validation unless a dependency is justified in a separate reviewed branch.

## Recommended Project Action

Do not add NPM dependencies for Codex UI, local agent wrappers, or simulator maintenance helpers.

For local maintainer machines, audit and remove any suspicious Codex-branded packages, especially `codexui-android`, and rotate affected credentials if such a package was installed.

## Update Warranted

CASE note warranted. Code, dependency, workflow, README, or SKILL changes are not warranted today.
