# 2026-07-04 - DWSIM AI Assistant MCP Tutorial Signal

## Sources

- DWSIM AI Assistant tutorial: https://dwsim.org/tutorials/en/features/ai-assistant.html
- DWSIM tutorials index: https://dwsim.org/tutorials/en/

## Summary / 摘要

中文：DWSIM 官方 AI Assistant 教程说明，Classic UI 中的 AI Assistant 可通过自然语言构建、修改和分析 flowsheet。教程明确指出，AI Assistant 背后调用与程序化使用相同的 MCP Server tools，例如 `dwsim.flowsheet.create`、`dwsim.thermo.add_compounds` 和 `dwsim.unitop.add`。教程还区分了使用场景：AI Assistant 适合快速探索、学习和调试；FluentAPI (Python) 更适合生产质量自动化和可重复、可版本控制的仿真；MCP Server (JSON-RPC) 适合自定义集成和工具化。

English: The official DWSIM AI Assistant tutorial explains that the Classic UI AI Assistant can build, modify, and analyze flowsheets through natural-language commands. It explicitly states that the assistant calls the same MCP Server tools used programmatically, such as `dwsim.flowsheet.create`, `dwsim.thermo.add_compounds`, and `dwsim.unitop.add`. The tutorial also separates use cases: AI Assistant for quick exploration, learning, and debugging; FluentAPI (Python) for production-quality automation and repeatable, version-controlled simulations; and MCP Server (JSON-RPC) for custom integrations and tooling.

## Relevance to AI-DWSIM-Skill / 与本项目的相关性

中文：这是高相关官方信号，因为它把 DWSIM AI Assistant、MCP Server 和 FluentAPI 的边界讲得更清楚。它支持本项目继续采用“AI/MCP 可做意图转译和辅助操作，但验收必须回到 DWSIM-native 计算、保存模型、FluentAPI/Automation3 重跑和机器可读导出”的原则。教程还提醒 AI Assistant 可能 hallucinate compound names，复杂规格可能超出工具能力，并建议为了可重复和版本控制，保存 flowsheet 后通过 FluentAPI 重跑。

English: This is a high-relevance official signal because it clarifies the boundary between DWSIM AI Assistant, MCP Server, and FluentAPI. It supports this repository's rule that AI/MCP can translate intent and assist operations, but acceptance must still rely on DWSIM-native calculation, saved models, FluentAPI/Automation3 re-runs, and machine-readable exports. The tutorial also warns that the AI Assistant can hallucinate compound names, complex specifications may exceed its tool repertoire, and repeatability/version control require saving the flowsheet and re-running via FluentAPI.

## Recommended Project Action / 建议项目动作

中文：新增 CASE 笔记，并在 README 的官方 DWSIM 参考中加入 Tutorials 和 AI Assistant tutorial 链接。不要把 AI Assistant、MCP Server 或 FluentAPI 设为本仓库安装依赖；不要声称本仓库已本地验证这些 DWSIM 10 功能。未来可做一个低风险示例：记录 AI Assistant/MCP 生成的候选模型，再用 DWSIM-native 计算和导出作为验收证据。

English: Add this CASE note and add the Tutorials and AI Assistant tutorial links to README's official DWSIM references. Do not make AI Assistant, MCP Server, or FluentAPI an installation dependency for this repository, and do not claim local validation of these DWSIM 10 features. A future low-risk example could record an AI Assistant/MCP-generated candidate model and then use DWSIM-native calculation and exports as acceptance evidence.

## Update Warranted / 是否需要更新

中文：需要新增 CASE 笔记和 README 官方参考链接。无需代码、依赖或 CI 变更。

English: CASE note and README official reference links warranted. No code, dependency, or CI change is warranted.
