# 2026-06-26 - Codex 0.142.2 MCP Discovery and PowerShell Safety

## Sources

- OpenAI Codex release 0.142.2: https://github.com/openai/codex/releases/tag/rust-v0.142.2
- OpenAI Codex changelog: https://developers.openai.com/codex/changelog

## Summary / 摘要

中文：OpenAI Codex 0.142.2 增强了 MCP 工具发现、远程 stdio MCP server 工作目录处理和 PowerShell 命令安全审批。发布说明中提到，MCP tools 在支持时默认使用 tool search；remote stdio MCP servers 可接受远程平台路径格式的绝对工作目录；包含安全分类器无法检查的可执行 AST 区域的 PowerShell 命令需要审批。

English: OpenAI Codex 0.142.2 improved MCP tool discovery, remote stdio MCP server working-directory handling, and PowerShell command safety approval. The release notes state that MCP tools use tool search by default when supported, remote stdio MCP servers can accept absolute working directories in the remote platform's path format, and PowerShell commands containing executable AST regions that the safety classifier cannot inspect require approval.

## Relevance to AI-DWSIM-Skill / 与本项目的相关性

中文：这不是 DWSIM 功能更新，也不能替代 DWSIM-native 计算验证。但它和 AI-DWSIM-Skill 的维护路径直接相关：本项目优先面向 Windows DWSIM 主机，验证入口使用 PowerShell，未来也可能通过 MCP adapter 连接 DWSIM host。远程 stdio MCP 的主机原生路径处理可以降低 Windows/Linux 路径错配风险；PowerShell 安全审批提醒我们继续保持验证脚本简单、可读、少动态执行。

English: This is not a DWSIM feature update and does not replace DWSIM-native calculation validation. It is directly relevant to AI-DWSIM-Skill maintenance because this repository prioritizes Windows DWSIM hosts, uses PowerShell for validation entry points, and may later connect to a DWSIM host through MCP adapters. Host-native remote stdio MCP working-directory handling can reduce Windows/Linux path mismatch risk, while PowerShell approval behavior reinforces the need to keep validation scripts simple, readable, and low on dynamic execution.

## Recommended Project Action / 建议项目动作

中文：仅记录 CASE。不要引入新的 MCP 依赖，也不要改变验证边界。后续如果新增 DWSIM MCP 示例或远程执行文档，应明确：

- MCP 是接口适配层，不是计算验收证据；
- 远程执行必须记录目标主机、shell、cwd 和 DWSIM 安装/DLL 路径；
- PowerShell 验证脚本应避免不可审计的动态 AST、字符串拼接执行和隐藏副作用；
- 接受结果前仍需 DWSIM-native 计算、solver status 和机器可读导出。

English: Record this as a CASE note only. Do not add a new MCP dependency or change the validation boundary. If a future DWSIM MCP example or remote-execution guide is added, it should state that MCP is an adapter rather than acceptance evidence; remote execution must record host, shell, cwd, and DWSIM installation/DLL paths; PowerShell validation scripts should avoid unauditable dynamic AST, string-built execution, and hidden side effects; and accepted results still require DWSIM-native calculation, solver status, and machine-readable exports.

## Update Warranted / 是否需要更新

中文：需要新增 CASE 笔记。无需代码、依赖、README 或 CI 变更。

English: CASE note warranted. No code, dependency, README, or CI change is warranted.
