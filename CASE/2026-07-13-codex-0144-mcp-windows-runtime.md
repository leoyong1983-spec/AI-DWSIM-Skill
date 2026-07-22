# 2026-07-13 - Codex 0.144.x MCP Auth and Windows Runtime Signal

## Sources

- OpenAI Codex release 0.144.0: https://github.com/openai/codex/releases/tag/rust-v0.144.0
- OpenAI Codex release 0.144.1: https://github.com/openai/codex/releases/tag/rust-v0.144.1
- DWSIM download page checked during the same heartbeat: https://dwsim.org/index.php/download/
- DWSIM AI Assistant tutorial checked during the same heartbeat: https://dwsim.org/tutorials/en/features/ai-assistant.html

## Summary / 摘要

中文：OpenAI Codex 0.144.x 发布说明显示，MCP tools 可以在不额外 opt-in 的情况下请求交互式认证，Windows sandbox sessions 可以在 writable roots 内删除文件并访问 managed primary runtime，终端错误恢复、Cloud Tasks 的 AGENTS.md 附加说明、code mode regression、`codex install` 流程和 MCP 提示处理也有修复。

English: OpenAI Codex 0.144.x reports that MCP tools can request interactive authentication without an extra opt-in, Windows sandbox sessions can delete files inside writable roots and access the managed primary runtime, and fixes landed for terminal error recovery, Cloud Tasks AGENTS.md append instructions, a code-mode regression, `codex install`, and MCP prompt handling.

## Relevance to AI-DWSIM-Skill / 与本项目的相关性

中文：这不是 DWSIM 功能更新，也不证明任何 DWSIM 模型已经运行。但它和本仓库维护方式直接相关：AI-DWSIM-Skill 以 Windows DWSIM 主机和 PowerShell 验证入口为优先路径；当目标机器缺少系统 Python 时，Codex managed runtime 可以作为维护期验证兜底，但不应成为项目安装依赖。MCP 交互认证也提醒未来 DWSIM MCP/remote-host 示例必须明确认证边界、token 存储、host、shell、cwd、DWSIM 安装路径、Automation DLL 路径和最终 DWSIM-native 计算导出。

English: This is not a DWSIM feature update and does not prove that any DWSIM model has run. It is directly relevant to repository maintenance because AI-DWSIM-Skill prioritizes Windows DWSIM hosts and PowerShell validation entry points. If a target machine lacks a system Python installation, the Codex managed runtime can be a maintainer-side validation fallback, but it should not become a project installation dependency. MCP interactive authentication also reinforces that future DWSIM MCP or remote-host examples must explicitly document authentication boundaries, token storage, host, shell, cwd, DWSIM installation paths, Automation DLL paths, and final DWSIM-native calculation exports.

## Recommended Project Action / 建议项目动作

中文：仅记录 CASE。不要把 Codex managed runtime、MCP 认证流程或 remote execution 作为 AI-DWSIM-Skill 安装依赖；不要改变 `Automation3`、已验证 runner、机器可读导出和 DWSIM-native 重跑的验收边界。未来如新增远程 DWSIM 主机说明，可把 managed runtime 写成“维护兜底验证工具”，而不是用户安装要求。

English: Record this as a CASE note only. Do not make the Codex managed runtime, MCP authentication flow, or remote execution an AI-DWSIM-Skill install dependency, and do not change the acceptance boundary based on `Automation3`, proven runners, machine-readable exports, and DWSIM-native reruns. If a future remote-DWSIM-host guide is added, describe the managed runtime as a maintainer-side validation fallback rather than a user installation requirement.

## Update Warranted / 是否需要更新

中文：需要新增 CASE 笔记。无需代码、README、SKILL、依赖或 CI 变更。

English: CASE note warranted. No code, README, SKILL, dependency, or CI change is warranted.
## 2026-07-23 Codex 0.145.0 Stable Update / 稳定版补充

- OpenAI Codex 0.145.0: https://github.com/openai/codex/releases/tag/rust-v0.145.0
- Evidence grade / 证据等级：B+。OpenAI 官方稳定版发布，可直接支持维护运行时判断；但不属于 DWSIM 原生计算验证。

中文：0.145.0 修复了慢速或冲突的 MCP 启动与认证流程，包括启动超时、非阻塞 OAuth 发现、凭据刷新串行化和工具目录安全复用；同时改进 Windows 原生执行服务器沙箱、网络代理强制、隐藏辅助控制台和 hook 命令引用。它还加强了危险删除检测、完整访问确认和审批拒绝原因保留。这些变化能降低本项目心跳在 Windows 上调用 MCP、执行校验和整理远程提交时的挂起、弹窗干扰与越权风险。

English: Codex 0.145.0 fixes slow or conflicting MCP startup and authentication through startup timeouts, non-blocking OAuth discovery, serialized credential refreshes, and safe tool-catalog reuse. It also improves Windows native exec-server sandboxing, network-proxy enforcement, hidden helper consoles, and hook-command quoting, while strengthening destructive-command detection, full-access confirmation, and approval-rejection provenance. These changes reduce hangs, visible helper-window interference, and permission ambiguity when this project's heartbeat invokes MCP tools, validates on Windows, or prepares remote repository updates.

Recommended action / 建议：记录并优先使用稳定版，不跟随 0.146.0 alpha 预发布。无需修改 DWSIM 控制逻辑或增加依赖；维护脚本继续保留明确超时、最小权限、可审计审批原因和 PowerShell 验证入口。DWSIM 运行成功仍必须由真实 DWSIM 环境及原生计算结果证明。
