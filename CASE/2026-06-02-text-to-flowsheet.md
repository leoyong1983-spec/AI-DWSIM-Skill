# 2026-06-02 - Text-to-Flowsheet for Automated Process Simulation

## Sources

- RSC Digital Discovery article page: https://pubs.rsc.org/en/Content/ArticleLanding/2026/DD/D6DD00060F
- DOI landing page: https://doi.org/10.1039/D6DD00060F

## Summary

The RSC Digital Discovery paper "Text-to-Flowsheet: expert-level digitization and automated simulation of chemical processes from natural language" describes a workflow that converts natural-language process descriptions into flowsheet graphs and then into rigorous process simulations.

The paper is relevant because it targets the same hard boundary AI-DWSIM-Skill is designed around: AI can propose or extract process topology and operating data, but executable simulation is still the validation gate. The work also emphasizes benchmarkable extraction quality and simulation readiness rather than treating an LLM-generated diagram as sufficient evidence.

## Relevance to AI-DWSIM-Skill

This is a high-relevance AI-assisted process simulation signal. It supports future DWSIM workflows where an agent may parse a text description, recover a candidate topology, create or update a minimum model, run DWSIM, and export convergence/status evidence.

It does not justify changing the control lane. For this repository, DWSIM native calculations through `Automation3`, proven runners, or a locally validated wrapper remain the authority layer. Any text-to-flowsheet workflow should still produce a loadable model, run calculation, and save machine-readable exports before claiming validation.

## Recommended Project Action

Add the paper to README's related AI process simulation references. Do not add a new dependency or planner framework today.

A future example could demonstrate a small text-to-DWSIM path:

- parse a short process description;
- build a minimum DWSIM model if no reusable model exists;
- run the model through the validated control lane;
- export topology, stream/equipment tables, solver status, and open assumptions.

## Update Warranted

CASE note warranted. README reference warranted. Code, dependency, or workflow changes are not warranted today.
