# 2026-05-27 - OntoLedgy DWSIM MCP Server Package

## Sources

- PyPI: ol-dwsim-mcp-server: https://pypi.org/project/ol-dwsim-mcp-server/
- GitHub: OntoLedgy/ol_dwsim_interop_services: https://github.com/OntoLedgy/ol_dwsim_interop_services
- DWSIM What's New: https://dwsim.org/index.php/whatsnew/

## Summary

The `ol-dwsim-mcp-server` package provides an MCP facade over DWSIM Automation, exposing flowsheet metadata, stream and unit-operation information, material-stream properties, compound constants, and thermodynamic calculations to MCP-capable clients such as Codex, Copilot Chat, Claude Desktop, and Cline.

The package is a third-party implementation, not the official DWSIM project. The PyPI package is marked beta, licensed under AGPL-3.0-or-later, and has a narrow DWSIM version requirement. Its documentation is still valuable because it demonstrates practical agent-facing DWSIM control patterns aligned with the official DWSIM MCP direction now listed on DWSIM's own "What's New" page.

## Relevance to AI-DWSIM-Skill

This is relevant as implementation evidence for:

- DWSIM-to-MCP agent integration
- safe read-only or calculation-oriented tool surfaces
- Python orchestration around DWSIM Automation
- JSON-RPC/MCP style interaction patterns for process simulation

It does not replace this repository's baseline. `Automation3`, proven local runners, and machine-readable exports remain the authority layer until a DWSIM MCP lane is locally installed, smoke-tested, and documented with explicit version and license boundaries.

## Recommended Project Action

Do not add `ol-dwsim-mcp-server` as a dependency. Its AGPL license and beta status make it unsuitable as a silent runtime dependency for this MIT-licensed skill.

Track it as a reference implementation. A future issue or branch could define a documentation-only evaluation checklist for DWSIM MCP tools: supported DWSIM version, license compatibility, writable vs read-only tool surfaces, local installation steps, smoke-test commands, and evidence export requirements.

## Update Warranted

CASE note warranted. Code or dependency change not warranted.
