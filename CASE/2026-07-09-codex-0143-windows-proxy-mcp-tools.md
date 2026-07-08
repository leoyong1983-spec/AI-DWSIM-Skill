# 2026-07-09 - Codex 0.143.0 Windows Proxy and MCP Tool Search Signal

## Sources

- OpenAI Codex release 0.143.0: https://github.com/openai/codex/releases/tag/rust-v0.143.0
- DWSIM download page checked during the same heartbeat: https://dwsim.org/index.php/download/
- DWSIM AI Assistant tutorial checked during the same heartbeat: https://dwsim.org/tutorials/en/features/ai-assistant.html

## Summary / 摘要

中文：OpenAI Codex 0.143.0 发布说明显示，remote plugins 默认启用，Codex 可以通过 macOS 和 Windows 系统代理路由认证与 Responses API 流量，并继续强化 MCP tool search 默认路径、ChatGPT-hosted MCP session authentication、remote-control pairing、Windows ConPTY 输入处理和远程执行恢复能力。

English: OpenAI Codex 0.143.0 reports that remote plugins are enabled by default, authentication and Responses API traffic can use macOS and Windows system proxies, and MCP tool search, ChatGPT-hosted MCP session authentication, remote-control pairing, Windows ConPTY input handling, and remote execution recovery were improved.

## Relevance to AI-DWSIM-Skill / 与本项目的相关性

中文：这不是 DWSIM 功能更新，也不构成 DWSIM 计算验证证据。但它与 AI-DWSIM-Skill 的维护和未来部署边界相关：本仓库面向 Windows DWSIM 主机，验证入口依赖 PowerShell，未来可能通过 MCP/remote executor 连接本地 DWSIM host。Windows 系统代理支持可降低企业网络下 Codex、GitHub、OpenAI API 访问失败的概率；MCP tool search 默认路径和 remote plugin 行为提醒我们继续把 MCP/插件视为维护侧或接口适配层，而不是安装依赖或验收证据。

English: This is not a DWSIM feature update and does not prove any simulator calculation. It is relevant to AI-DWSIM-Skill maintenance and deployment boundaries because this repository targets Windows DWSIM hosts, uses PowerShell validation entry points, and may later connect to local DWSIM hosts through MCP or remote executors. Windows system proxy support can reduce enterprise-network failures for Codex, GitHub, and OpenAI API access. MCP tool-search defaults and remote-plugin behavior reinforce that MCP/plugins should remain maintainer-side or adapter layers, not install-time dependencies or acceptance evidence.

## Recommended Project Action / 建议项目动作

中文：仅记录 CASE。不要为本仓库新增 Codex、MCP 或 remote plugin 运行时依赖；不要改变 DWSIM-native 验收边界。后续如新增远程 DWSIM 主机或 MCP 示例，应明确记录 host、shell、cwd、代理/网络边界、DWSIM 安装路径、Automation DLL 路径、MCP server 身份、模型 workcopy 和最终 DWSIM-native 计算导出。

English: Record this as a CASE note only. Do not add Codex, MCP, or remote-plugin runtime dependencies to this repository, and do not change the DWSIM-native acceptance boundary. If future remote-DWSIM-host or MCP examples are added, record the host, shell, cwd, proxy/network boundary, DWSIM installation path, Automation DLL path, MCP server identity, model workcopy, and final DWSIM-native calculation exports.

## Update Warranted / 是否需要更新

中文：需要新增 CASE 笔记。无需代码、README、SKILL、依赖或 CI 变更。

English: CASE note warranted. No code, README, SKILL, dependency, or CI change is warranted.
