# 2026-07-17 - GitHub MCP 1.6 and Codex Command-Safety Signal

## Source Links

- GitHub MCP Server v1.6.0: https://github.com/github/github-mcp-server/releases/tag/v1.6.0
- OpenAI Codex 0.144.5: https://github.com/openai/codex/releases/tag/rust-v0.144.5

## Summary

中文：GitHub MCP Server v1.6.0 新增可选 `fields` 参数，用于让模型只请求需要的返回字段，从而缩小工具响应并节省上下文；该能力目前主要位于 Insiders 模式。版本还为部分 issue 指派和关闭操作增加 `rationale`、`confidence` 或建议标记，并强化父子 issue 信息与输入清理。OpenAI Codex 0.144.5 同期扩展危险命令检测，并在拒绝命令时给出更清晰的原因。

English: GitHub MCP Server v1.6.0 adds an optional `fields` parameter so models can request only the response fields they need, reducing tool-response size and context use; the capability is currently centered on Insiders mode. The release also adds rationale, confidence, or suggestion metadata to selected issue-assignment and issue-closing operations, enriches issue hierarchy signals, and strengthens input sanitization. OpenAI Codex 0.144.5 concurrently expands dangerous-command detection and provides clearer rejection reasons.

## Relevance to AI-DWSIM-Skill

中文：这些是维护侧信号，不是 DWSIM 功能更新，也不能作为模拟计算验证证据。字段过滤有助于每日心跳在检查 PR、issues、CI 和代码时减少无关上下文；操作理由与置信度字段有助于审计自动 issue 决策；更严格的危险命令识别与本项目保持小范围、可审查、非破坏性维护的规则一致。

English: These are maintainer-side signals, not DWSIM feature updates, and they do not prove simulator calculations. Field filtering can reduce irrelevant context when the heartbeat checks pull requests, issues, CI, and code. Rationale and confidence metadata can improve auditability of automated issue decisions. Stronger dangerous-command detection aligns with this repository's small, reviewable, non-destructive maintenance policy.

## Recommended Project Action

中文：仅记录 CASE。不要把 GitHub MCP Server 或 Codex 作为本仓库安装依赖。未来若维护环境提供这些能力，可优先请求最小字段集，并在自动关闭、指派或创建 issue 时保存理由、置信度和来源；任何 DWSIM 模型结论仍必须由 DWSIM-native 计算、solver 状态和机器可读导出证明。

English: Record this as a CASE note only. Do not add GitHub MCP Server or Codex as repository installation dependencies. If these capabilities become available in the maintainer environment, request the smallest useful field set and preserve rationale, confidence, and sources for automated issue creation, assignment, or closure. Any DWSIM model conclusion must still be proven by DWSIM-native calculation, solver status, and machine-readable exports.

## Project Update Decision

中文：需要文档更新，仅新增本 CASE；不修改代码、依赖、README、SKILL 或验证边界。

English: A documentation-only update is warranted: add this CASE note, with no code, dependency, README, SKILL, or validation-boundary change.
## 2026-07-24 GitHub MCP Server 1.7.0 Stable Update / 稳定版补充

- Official release / 官方发布：https://github.com/github/github-mcp-server/releases/tag/v1.7.0
- Evidence grade / 证据等级：B+。GitHub 官方稳定版，可直接支持仓库维护工具链判断；不属于 DWSIM 原生运行证据。

中文：1.7.0 为 stdio 模式增加 GitHub App 服务间认证，改善 lockdown 作者检查，并为 label_write 写标签工具补充破坏性操作提示。它还纳入面向 MCP 2026-07-28 规范的 SDK 预发布兼容变化。对无人值守心跳而言，GitHub App 的短期安装令牌比长期个人访问令牌更适合作为未来认证路径，破坏性提示和集中 lockdown 检查也能降低误写风险。

English: Version 1.7.0 adds GitHub App server-to-server authentication over stdio, centralizes lockdown author checks, and marks label_write with a destructive-operation hint. It also includes SDK changes targeting the MCP 2026-07-28 specification. For unattended maintenance, short-lived GitHub App installation tokens are a safer future path than long-lived personal access tokens, while destructive hints and centralized lockdown checks reduce accidental writes.

Recommended action / 建议：当前继续使用已验证的 gh 认证与最小权限分支/PR 流程，不新增 MCP 运行依赖。未来若心跳迁移到 GitHub MCP，应优先评估 GitHub App 认证、固定稳定版本、限制可写工具集，并要求分支、CI 和人工可审查 PR。MCP 规范兼容声明不能替代真实 DWSIM 计算验证。
