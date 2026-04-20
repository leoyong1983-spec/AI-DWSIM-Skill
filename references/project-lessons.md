# 项目经验沉淀

以下规则来自当前绿色甲醇项目的实际执行链，而不是纸面建议。

## 1. 先接管环境，再谈建模

第一关必须是：

- DWSIM 安装路径
- Automation DLL
- `Automation3` 反射或加载
- `Create / Load / Calculate / Save` roundtrip smoke test

这关没过，不进入模型层。

## 2. 稳定主通道是 `.NET Automation3 + 编译型 runner + PowerShell 包装`

当前项目验证过，最稳的控制通道是：

- `.NET Automation3`
- C# runner
- PowerShell 外层包装

`COM` 只有在本机实测可用时才启用，不能靠注册表猜。

## 3. 母版模型按真实重跑结果选，不按文件名选

排序依据应该是：

1. `Load / Calculate / Save` 是否都通过
2. 异常数
3. 最近分支
4. 关键产量读数
5. 是否已有审计链

## 4. 必须区分加载失败和求解失败

至少要分开记录：

- 加载失败
- 对象绑定失败
- 求解失败
- 已收敛但结果不合理

否则后续会误判问题层级。

## 5. 只有全部候选母版失败，才进入 MVP 路线

当现有模型都不可用时，最小可运行模型应该遵循：

- once-through
- 不先闭 recycle
- 不改核心反应器路线
- 不堆复杂设备

## 6. 调优必须分层推进

推荐顺序：

1. `throughput / feed consistency`
2. `purge / recycle`
3. `热集成 / 反应器入口窗口`
4. `压力微调`

禁止多参数乱扫。

## 7. 调优边界必须冻结

默认冻结：

- Property Package
- 核心反应器路线
- 主拓扑
- 反应集
- 已验证收敛结构

技能默认追求“最小改动过线”，不是自由性能冲刺。

## 8. 调优成果不能直接升级为正式基线

应先过受限 gate。

本项目有效做法是：

- 用 `throughput-only gate` 证明在不改拓扑前提下跨过目标线
- 再冻结正式基线

## 9. 正式基线冻结必须包含三件事

1. 将 accepted case 克隆为 `baseline`
2. 做 `save / reload / headless recalc` 再验证
3. 另做只改坐标和标签的人类友好 layout 副本

## 10. 每次自动运行都必须留机器可审计工件

至少包括：

- `headless_results.json`
- `key_streams.csv`
- `key_equipment.csv`
- `assumptions`
- `open_issues`
- `status`

不要把“成功”只留在聊天里。

## 11. 发布门禁必须做三方一致性检查

对齐：

1. frozen model object inventory
2. exported equipment list
3. package narrative

任一不一致都应立刻登记为 `RB-xx`，并切到 `Blocker Resolution Mode`。

## 12. 人工裁决项必须独立管理

AI 可以：

- 整理证据
- 回填口径
- 把裁决传递到包件

AI 不可以：

- 自行关闭人工裁决项

像这些都必须等待显式最终裁决：

- 热负荷单位
- layout sign-off
- 元素衡算口径
- 包深度边界
