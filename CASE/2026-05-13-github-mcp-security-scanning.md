# 2026-05-13 - GitHub MCP Security Scanning for Agentic Maintenance

## Sources

- GitHub Changelog: Dependency scanning with GitHub MCP Server is in public preview: https://github.blog/changelog/2026-05-05-dependency-scanning-with-github-mcp-server-is-in-public-preview/
- GitHub Changelog: Secret scanning in AI coding agents via the GitHub MCP Server: https://github.blog/changelog/2026-03-17-secret-scanning-in-ai-coding-agents-via-the-github-mcp-server/
- GitHub MCP Server: https://github.com/github/github-mcp-server
- GitHub Docs: Extending Copilot Chat with MCP: https://docs.github.com/en/copilot/customizing-copilot/extending-copilot-chat-with-mcp

## Summary

GitHub is expanding the official GitHub MCP Server with security-oriented capabilities, including Dependabot vulnerability scanning in public preview and secret scanning for AI coding agent workflows. This matters for repositories that use AI coding agents because the agent can inspect repository security signals through a narrower, auditable GitHub interface instead of relying only on manual web checks or broad token-driven scripts.

The signal is maintenance-infrastructure related, not DWSIM-runtime related. It does not change the recommended control lane for process simulation. `Automation3`, validated runners, and machine-readable exports remain the authority layer for DWSIM work.

## Relevance to AI-DWSIM-Skill

This repository already has a daily maintenance heartbeat, Dependabot configuration, GitHub Actions validation, and a policy of recording valuable external signals under `CASE/`. GitHub MCP security scanning is relevant because it can improve future agent triage of:

- dependency alerts
- secret scanning alerts
- security hygiene before merging maintenance PRs
- explicit blocker reporting when credentials or permissions are insufficient

## Recommended Project Action

Do not add the GitHub MCP Server as a project dependency. It is a maintainer-side tool, not a runtime requirement for the skill.

For future maintenance automation, consider using the GitHub MCP Server or GitHub connector as an optional authority source when checking Dependabot and secret scanning status. Keep any such usage outside the install path for AI-DWSIM-Skill, and continue to avoid committing secrets, private simulation data, or personal machine paths.

## Update Warranted

CASE note warranted. Code change not warranted.
