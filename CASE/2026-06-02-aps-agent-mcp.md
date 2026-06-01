# 2026-06-02 - APS Agent via Model Context Protocol

## Sources

- arXiv: Large Language Model Agent for User-friendly Chemical Process Simulations: https://arxiv.org/abs/2601.11650
- Model Context Protocol documentation: https://modelcontextprotocol.io/docs

## Summary

The APS Agent paper describes an LLM agent interface for AVEVA Process Simulation that uses the Model Context Protocol to connect natural-language interaction with process simulation actions. The paper is not DWSIM-specific, but it is relevant as evidence that process-simulation agents are moving toward standardized, tool-mediated interfaces rather than prompt-only workflows.

The MCP documentation defines a protocol layer for exposing tools and context to model-driven clients. For simulator control, this supports a useful separation: the LLM requests a typed action, and a local server handles simulator-specific execution, locking, state capture, and result return.

## Relevance to AI-DWSIM-Skill

This signal supports the project's existing direction: use AI for orchestration and reasoning, but keep simulator execution, version logging, and machine-readable outputs as the validation gate.

It also reinforces the need to evaluate DWSIM MCP-style tools carefully. An MCP server can make simulator access easier, but it does not remove the need for local DWSIM installation checks, model workcopies, mutating-tool boundaries, solver-status exports, and cleanup behavior.

## Recommended Project Action

Document MCP as an optional future tool boundary, not a default dependency. Any DWSIM MCP lane should be accepted only after smoke tests prove version support, tool schema, read/write behavior, queue or lock behavior, and exported evidence paths.

For optimization or text-to-flowsheet use cases, keep the acceptance gate unchanged: DWSIM-native calculation and exported solver status are required before reporting a case as validated.

## Update Warranted

CASE note warranted. Reference-document update warranted. Code, dependency, or workflow changes are not warranted today.
