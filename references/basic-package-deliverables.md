# 基础工艺包交付层规则

## 目标

本技能的目标不是单独跑一个模型，而是把 DWSIM 运行结果稳定转成可审计、可发审的基础工艺包交付链。

## 默认交付分层

### 1. 机器可读源层

先导出：

- `headless_results.json`
- `key_streams.csv`
- `key_equipment.csv`
- `utility_summary.csv`
- assumptions / open issues / status

这是正式 `Word / Excel / PowerPoint` 的数据源层。

### 2. 正式 Office 层

当项目进入基础工艺包审查版时，正式件通常包括：

- 物料衡算工作簿
- 热量衡算工作簿
- 主要设备表
- 公用工程汇总表
- 人工裁决登记表
- 基础工艺包主文档
- 交付清单与问题说明
- 发审传递说明
- 审查汇报稿

### 3. 审查支持层

进入发审准备后，应补齐：

- 审查会首读指南
- 发审范围说明
- 审查意见登记表
- 审查意见闭环流程
- 工艺流程图后补范围说明

## 默认边界

如果项目已进入 review-stage basic process package：

- 不再继续自由调优
- 不再改 frozen baseline
- layout 副本只允许视觉微调
- 独立 `PFD-ready` 图件可以作为后补接口，但不能伪装成已完成

## 发布门禁

### Release blocker

如果发现冻结模型真实对象，与导出层或包件文本不一致：

1. 立即建立 `RB-xx`
2. 暂停新增增强类工作
3. 优先做只读 inventory、export refresh、package 刷新和 QA 复核

### 人工裁决

可以存在 `OPEN human decisions`，但不能存在“未登记的 release blocker”。

## 中文交付约束

如果项目要求中文交付：

- 提交目录名、文件名、读者可见正文优先中文
- 必要英文术语应加中文注释
- 路径、位号、流股标签、API 名称可保留
- 不要把终端显示乱码误判为文件真损坏
