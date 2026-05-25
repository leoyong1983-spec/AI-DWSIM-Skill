# 2026-05-26 - DWSIM MCP Server and Fluent API

## Sources

- DWSIM What's New: https://dwsim.org/index.php/whatsnew/
- DWSIM tutorials: https://dwsim.org/tutorials/en/index.html

## Summary

The official DWSIM "What's New" page now lists several 2026 AI and automation features that are directly relevant to AI-DWSIM-Skill:

- DWSIM MCP Server, exposing flowsheets, streams, unit operations, and thermodynamic calculations to external LLM agents.
- AI Code Assist for generating and editing Python script code with a diff viewer.
- AI-assisted heat exchanger sizing.
- Fluent API in Python/VB.NET for programmatic flowsheet automation, with contextual help and validation tests.

The official tutorial index also says tutorials include two automation paths: FluentAPI in Python and MCP Server through JSON-RPC prompts.

## Relevance to AI-DWSIM-Skill

This is a high-relevance official signal because it narrows the gap between external AI agents and DWSIM-native automation. It supports the project's direction: AI agents should interact with DWSIM through documented, scriptable interfaces and should keep simulator execution as the acceptance gate.

It does not immediately replace the existing control-lane policy. `Automation3`, proven project runners, and machine-readable exports remain the authority layer until the DWSIM MCP Server and Fluent API are locally exercised in this repository's validation environment.

## Recommended Project Action

Track DWSIM MCP Server and Fluent API as official optional integration lanes. Do not add dependencies or claim runtime support yet.

A future low-risk branch could add a documentation-only control-lane matrix entry for:

- `Automation3` as the current authority lane.
- Fluent API as an official scripting lane to validate when available.
- DWSIM MCP Server as an LLM-agent lane to validate through JSON-RPC smoke tests.

## Update Warranted

CASE note warranted. README boundary note warranted. Code change not warranted.
