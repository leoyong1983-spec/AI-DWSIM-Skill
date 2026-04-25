# 2026-04-26 - Sketch2Simulation Multi-Agent Flowsheet Generation

## Sources

- arXiv: Sketch2Simulation: Automating Flowsheet Generation via Multi Agent Large Language Models: https://arxiv.org/abs/2603.24629
- arXiv: Context is all you need: Towards autonomous model-based process design using agentic AI in flowsheet simulations: https://arxiv.org/abs/2603.12813

## Summary

The March 2026 Sketch2Simulation preprint presents an end-to-end multi-agent LLM workflow that converts process diagrams into executable Aspen HYSYS flowsheets. The system separates diagram parsing, graph-based intermediate representation construction, simulator code generation, execution, and structural verification.

The paper reports successful executable HYSYS model generation across four chemical engineering case studies, from a simple desalting process to an aromatic production flowsheet with recycle loops. It also highlights remaining weaknesses around dense recycle structures, implicit diagram semantics, and simulator-interface constraints.

## Relevance to AI-DWSIM-Skill

The simulator target is Aspen HYSYS rather than DWSIM, but the architecture maps closely to future AI-DWSIM-Skill opportunities:

- parse a process requirement or diagram into an explicit intermediate representation
- generate simulator-control code through a narrow automation lane
- run the simulator rather than trusting generated code by inspection
- verify structure, streams, and convergence before accepting the output

This reinforces the value of keeping DWSIM control script-first and auditable. It also suggests that future DWSIM examples should separate process intent, topology representation, Automation3 execution, and validation logs instead of blending them into one prompt.

## Recommended Project Action

Add the paper to the README's related AI process simulation references.

Consider a future issue for a small DWSIM-oriented example that starts from a structured PFD-like description and verifies generated topology through Automation3. Do not add a new dependency or image-parsing stack yet.

## Update Warranted

Documentation update warranted. Code change not warranted.
