# 2026-06-28 - DWSIM 10 Security Audit and Validation Package Signal

## Sources

- DWSIM download page: https://dwsim.org/index.php/download/
- DWSIM Security Audit PDF: https://mrabcdkqjhotmejgtayb.supabase.co/storage/v1/object/public/dwsim-files/SECURITY_AUDIT.pdf
- DWSIM Model Validation Report PDF: https://mrabcdkqjhotmejgtayb.supabase.co/storage/v1/object/public/dwsim-files/DWSIM_Validation_Report.pdf

## Summary / 摘要

中文：DWSIM 官方下载页显示 DWSIM 10 Patreon Edition 已更新到 `v10.0.9671.22365`，并公开提供 Security Audit 和 Model Validation Report 入口。Security Audit 面向企业 IT / Security 团队，说明 DWSIM、Patreon Extensions 和 `dwsim-assistant` 的安装 footprint、网络行为、本地监听、凭据存储、代码执行面、第三方组件、数据外发和防火墙建议。

English: The official DWSIM download page lists DWSIM 10 Patreon Edition `v10.0.9671.22365` and exposes Security Audit and Model Validation Report links. The Security Audit is aimed at corporate IT/Security teams and documents the install footprint, network behavior, local listeners, credential storage, code-execution surface, third-party components, outbound data, and firewall guidance for DWSIM, Patreon Extensions, and `dwsim-assistant`.

## Relevance to AI-DWSIM-Skill / 与本项目的相关性

中文：这不是新的开源基线，也不是 Automation3 运行验证证据，但它对 AI-DWSIM-Skill 的工业部署边界很有价值。审计材料明确指出 `dwsim-assistant` 是可选 AI helper，本地服务绑定 localhost 并使用 per-launch token；云 LLM、Supabase、MCP servers、OPC UA 和 PI Data Archive 都属于用户配置或可选外联/读访问路径。这支持本项目继续把 AI/MCP/GUI assistant 视为适配和辅助层，而不是替代 DWSIM-native 计算和机器可读导出的验收层。

English: This is not a new open-source baseline and does not prove Automation3 runtime validation, but it is highly relevant to AI-DWSIM-Skill's industrial deployment boundary. The audit states that `dwsim-assistant` is an optional AI helper, local services bind to localhost and use per-launch tokens, and cloud LLMs, Supabase, MCP servers, OPC UA, and PI Data Archive access are user-configured or optional outbound/read paths. This reinforces the repository's position that AI/MCP/GUI assistant features are adapters and support layers, not replacements for DWSIM-native calculations and machine-readable acceptance evidence.

## Recommended Project Action / 建议项目动作

中文：仅记录 CASE。不要把 DWSIM 10 Patreon Edition、Security Audit、Model Validation Report 或 `dwsim-assistant` 设为安装依赖。未来如编写企业部署指南或远程 DWSIM host 示例，应引用这些材料并要求：

- 明确是否启用云 LLM、Supabase logging、MCP servers、OPC UA、PI Data Archive 和 convergence upload；
- 默认记录主机、shell、cwd、DWSIM 版本、Automation DLL 路径、本地监听和网络出站边界；
- 对模型修改继续使用 workcopy；
- 接受结果前仍需 DWSIM-native solver status、导出文件和可复核日志。

English: Record this as a CASE note only. Do not make DWSIM 10 Patreon Edition, the Security Audit, the Model Validation Report, or `dwsim-assistant` an installation dependency. If a future enterprise deployment guide or remote DWSIM host example is written, reference these materials and require explicit disclosure of cloud LLM, Supabase logging, MCP servers, OPC UA, PI Data Archive, and convergence upload settings; record host, shell, cwd, DWSIM version, Automation DLL path, local listeners, and outbound network boundaries; keep model mutations on workcopies; and still require DWSIM-native solver status, exports, and auditable logs before accepting results.

## Update Warranted / 是否需要更新

中文：需要新增 CASE 笔记。无需代码、依赖、README 或 CI 变更。

English: CASE note warranted. No code, dependency, README, or CI change is warranted.
