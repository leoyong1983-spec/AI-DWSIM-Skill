# 2026-05-22 - DWSIM Official Integration and AI-Relevant Components

## Sources

- DWSIM downloads: https://dwsim.org/index.php/download/
- DWSIM Automation3 API: https://dwsim.org/api_help/html/T_DWSIM_Automation_Automation3.htm
- DWSIM OPC UA Client Plugin: https://dwsim.org/wiki/index.php?title=OPC_UA_Client_Plugin
- DWSIM Neural Network Unit Operation: https://dwsim.org/wiki/index.php?title=Neural_Network_Unit_Operation

## Summary

The official DWSIM download page now lists Patreon Edition `v10.0.9636.15918`, published on 2026-05-20. The same page also links DWSIM 10 security-audit and model-validation documents, plus official additional components including the OPC UA Client Plugin and the Neural Network Unit Operation.

The public Automation3 API documentation continues to show `DWSIM.Automation.dll` version `10.0.0.0`. The API surface still includes the methods this project treats as the authoritative automation lane, including flowsheet creation/loading, calculation, saving, version discovery, and resource cleanup.

The OPC UA Client Plugin is relevant to industrial integration because it maps OPC variables to DWSIM simulation objects and can automatically recalculate the flowsheet when values change. The Neural Network Unit Operation is relevant to AI-assisted simulation because it supports fitting and evaluating a multi-input, multi-output neural-network unit operation inside DWSIM.

## Relevance to AI-DWSIM-Skill

This signal is highly relevant but does not require a control-lane change:

- `Automation3` remains the default execution and audit authority.
- Official DWSIM security and validation documents are useful trust evidence for enterprise or review-stage environments.
- OPC UA integration is a potential future lane for digital-twin or plant-data coupling.
- The Neural Network Unit Operation is a DWSIM-native AI-adjacent capability, but it should remain optional and simulator-native rather than becoming a new project dependency.

## Recommended Project Action

Keep the project version-agnostic and continue logging the resolved DWSIM version, Automation DLL path, and control lane during readiness checks.

Do not add OPC UA or neural-network examples yet. A future issue could define a bounded example that reads or writes a small set of variables through a proven integration lane, then records convergence status and exported evidence.

## Update Warranted

CASE note warranted. Code change not warranted.
