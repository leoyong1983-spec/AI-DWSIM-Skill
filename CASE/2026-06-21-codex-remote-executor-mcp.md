# 2026-06-21 - Codex Remote Executor and Per-Thread MCP Signal

## Sources

- OpenAI Codex changelog: https://developers.openai.com/codex/changelog
- Codex releases: https://github.com/openai/codex/releases

## Summary / 摘要

中文：OpenAI Codex 在 2026-06-18 附近的更新中强化了远程执行器和本地/远程线程迁移能力，并提到跨平台执行时保留目标主机的工作目录、shell 和环境习惯。更新还支持在选定执行器插件时按线程启动 stdio MCP servers。

English: Around June 18, 2026, OpenAI Codex updates strengthened remote executors and local/remote thread handoff. The update notes cross-platform execution that preserves the target host's working directory, shell, and environment conventions, and adds per-thread stdio MCP server activation when executor plugins are selected.

## Relevance to AI-DWSIM-Skill / 与本项目的相关性

中文：这不是 DWSIM 官方功能，也不能证明任何 DWSIM 模型已运行。但它与 AI-DWSIM-Skill 的维护和未来部署方式相关：DWSIM 真实计算通常依赖 Windows 本机安装、`.NET/Automation3` DLL、PowerShell、路径和许可证环境；如果 Codex 线程可以在本地 Windows DWSIM 主机与远程维护环境之间更安全地迁移，就能减少“仓库 CI 通过但本地 DWSIM 未验证”的断层。

English: This is not a DWSIM feature and does not prove that any DWSIM model has run. It is relevant to AI-DWSIM-Skill maintenance and future deployment because real DWSIM calculations often depend on a Windows host, `.NET/Automation3` DLLs, PowerShell, local paths, and licensing/runtime state. Safer handoff between a local Windows DWSIM host and remote maintenance environment can reduce the gap between repository CI and real simulator validation.

## Recommended Project Action / 建议项目动作

中文：仅记录 CASE。不要把 remote executor 或 MCP 作为本仓库安装依赖，也不要放宽现有验证边界。后续如果实际使用该能力，应要求：

- 明确当前执行主机、shell、工作目录和 DWSIM 安装路径；
- 对任何模型修改使用 workcopy；
- 通过 DWSIM-native 计算和机器可读导出证明结果；
- 将 MCP 工具视为接口适配层，而不是验证证据本身。

English: Record this as a CASE note only. Do not make remote executors or MCP an install-time dependency, and do not loosen the repository's validation boundary. If this capability is used later, require explicit host, shell, working directory, and DWSIM installation evidence; operate on workcopies for model mutations; prove results through DWSIM-native calculation and machine-readable exports; and treat MCP as an interface adapter rather than validation evidence.

## Update Warranted / 是否需要更新

中文：需要新增 CASE 笔记。无需代码、依赖、README 或 CI 变更。

English: CASE note warranted. No code, dependency, README, or CI change is warranted.
