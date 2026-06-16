# 2026-06-17 - LLMs in Process Systems Engineering Survey

## Sources

- arXiv abstract: https://arxiv.org/abs/2606.11589
- arXiv PDF: https://arxiv.org/pdf/2606.11589

## Summary / 摘要

中文：2026-06-10 发布的综述论文 "Large Language Models in Process Systems Engineering: Opportunities, Architectures, and Industrial Deployment Challenges" 系统整理了 LLM 在过程系统工程中的应用方向，包括过程设计与工程、分子设计、过程建模与模拟、时间序列预测、优化调度、过程控制、故障检测与诊断。

English: The June 10, 2026 survey "Large Language Models in Process Systems Engineering: Opportunities, Architectures, and Industrial Deployment Challenges" reviews LLM applications across process design, molecular design, process modeling and simulation, forecasting, optimization and scheduling, process control, and fault detection and diagnosis.

## Relevance to AI-DWSIM-Skill / 与本项目的相关性

中文：该论文的价值不在于提供新的 DWSIM API，而在于强化本项目已经采用的边界：LLM 适合做文档查询、非结构化知识综合、人机交互和候选方案生成；但涉及实时执行、约束满足、收敛、安全保证和工程验收时，必须回到可执行模拟、优化器、日志和机器可读证据。

English: The paper does not introduce a new DWSIM API. Its value is that it reinforces this repository's existing boundary: LLMs are useful for documentation queries, unstructured knowledge synthesis, human-machine interaction, and candidate generation, but real execution, constraint satisfaction, convergence, safety guarantees, and engineering acceptance still require simulator-backed runs, optimizers, logs, and machine-readable evidence.

## Recommended Project Action / 建议项目动作

中文：将该综述加入 README 的相关 AI process simulation 参考列表。暂不引入新的 agent 框架、调度系统或实时控制接口。未来如要扩展，可优先做一个小型示例：LLM 生成候选建模意图，DWSIM Automation3 或已验证 runner 执行计算，导出状态、流股、设备和未关闭假设。

English: Add the survey to README's related AI process simulation references. Do not add a new agent framework, scheduler, or real-time control interface today. A future example could let an LLM generate candidate modeling intent while DWSIM Automation3 or a validated runner performs the calculation and exports status, stream, equipment, and open-assumption evidence.

## Update Warranted / 是否需要更新

中文：需要新增 CASE 笔记和 README 参考。无需代码、依赖或 CI 变更。

English: CASE note and README reference warranted. No code, dependency, or CI change is warranted.
