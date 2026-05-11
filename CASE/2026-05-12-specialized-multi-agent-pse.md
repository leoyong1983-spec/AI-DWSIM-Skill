# 2026-05-12 - Specialized Multi-Agent LLMs for Process Systems Engineering

## Sources

- ScienceDirect: Improving process systems engineering with specialized multi-agent large language models: https://www.sciencedirect.com/science/article/pii/S2666821126001109
- DOI: https://doi.org/10.1016/j.ceja.2026.101141
- Code repository: https://github.com/fearrais96/Applications-of-LLMs-in-PSE

## Summary

The May 2026 Chemical Engineering Journal Advances paper evaluates specialized multi-agent LLM workflows on process systems engineering tasks. The case studies cover soft sensing from ATR-FTIR data, dynamic mechanistic modeling through population balance models, and nonlinear model predictive control for batch crystallization.

The paper is not DWSIM-specific. Its value for this repository is the experimental evidence that process-engineering agents need domain-specific task decomposition, model validation, and executable engineering checks. That aligns with AI-DWSIM-Skill's current script-first posture: an AI agent may reason about process design, but DWSIM Automation3, validated runners, calculation logs, and exported artifacts remain the authority layer.

## Relevance to AI-DWSIM-Skill

This signal is relevant to:

- AI agents for process simulation and control
- soft-sensor, dynamic-modeling, and NMPC workflows
- validation-first engineering automation
- separating LLM reasoning from simulator-backed acceptance gates
- future DWSIM examples that combine bounded optimization with auditable exports

## Recommended Project Action

Add this paper to the README's related AI process simulation references. Do not add a new multi-agent framework, Dyad dependency, or NMPC implementation yet.

The practical next step is a small future example or issue: define a bounded DWSIM sensitivity or optimization task where the agent proposes candidate settings, Automation3 executes the model, and repository scripts record the acceptance evidence.

## Update Warranted

CASE note warranted. README reference warranted. Code change not warranted.
