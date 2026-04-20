# 权威路径与执行通道选择

## 结论先行

把 DWSIM 交给 AI 控制时，推荐把执行通道分成两层：

1. `Automation3 / .NET / C# runner` 作为主控制层
2. `Python` 作为 orchestration 层、参数注入层、批量试算层或优化层

不要反过来。

原因很简单：

- Daniel Medeiros 维护的 DWSIM 官方仓库里，自动化测试示例直接证明了 `Automation3` 可以完成 `CreateFlowsheet`、`LoadFlowsheet`、`CalculateFlowsheet2`、`SaveFlowsheet`
- `dwsimopt` 这类 Python 库证明了“把 DWSIM 嵌进 Python 工作流”是可行的，但它本质上仍然是建立在 DWSIM DLL 可访问的前提上
- 对 AI 代理来说，最稳的方案不是“纯 Python 幻想控制”，而是“底层用官方 Automation3，外层用 Python 或 PowerShell 驱动回合、调优、导出和包件编制”

## 外部权威依据

### 1. Daniel / DWSIM 官方自动化示例

官方仓库：

- [DanWBR/dwsim](https://github.com/DanWBR/dwsim)
- [DWSIM.Automation.Tests.CSharp](https://github.com/DanWBR/dwsim/tree/windows/DWSIM.Automation.Tests.CSharp)

关键示例：

- [`newAPI.cs`](https://raw.githubusercontent.com/DanWBR/dwsim/windows/DWSIM.Automation.Tests.CSharp/newAPI.cs)
- [`looptest.cs`](https://raw.githubusercontent.com/DanWBR/dwsim/windows/DWSIM.Automation.Tests.CSharp/looptest.cs)
- [`Module1.vb`](https://raw.githubusercontent.com/DanWBR/dwsim/windows/DWSIM.Automation.Tests/Module1.vb)

这些示例直接展示了：

1. 通过 `new DWSIM.Automation.Automation3()` 创建自动化管理器
2. 通过 `CreateFlowsheet()` 新建模型
3. 通过 `LoadFlowsheet()` 打开现有模型
4. 通过 `CalculateFlowsheet2(sim)` 触发求解
5. 通过 `SaveFlowsheet(...)` 或 `SaveFlowsheet2(...)` 保存模型
6. 通过对象接口修改流股属性和读取结果

这条路径最适合作为主执行通道，因为它最贴近 DWSIM 自身的真实 API 设计。

### 2. Python 便利层：dwsimopt

仓库：

- [lf-santos/dwsimopt](https://github.com/lf-santos/dwsimopt)
- README: [raw](https://raw.githubusercontent.com/lf-santos/dwsimopt/master/README.md)

README 的关键信号：

1. 它自称是“automates DWSIM simulations for process optimization”的 Python 库
2. 目标是把 DWSIM 仿真嵌入 Python 工作流
3. 依赖对 DWSIM DLL 的访问
4. 带有明显的环境约束，包括 Python 版本和 `pythonnet`

对 AI 代理的含义是：

- 适合做参数扫描、优化、目标函数和约束包装
- 不应取代底层官方 Automation API 的权威地位
- 在现代 Windows 现场环境中，必须先验证它的依赖是否兼容，再决定是否启用

## 推荐执行通道

### 主通道

`PowerShell / Python orchestration -> C# runner or direct .NET Automation3 -> DWSIM`

适用场景：

- 环境接管
- smoke test
- 现有模型加载
- 计算与收敛
- 冻结基线
- 导出 `CSV / JSON / Markdown`
- 发布门禁核查

### 辅助通道

`Python wrapper -> DWSIM DLL / automation layer`

适用场景：

- 单变量或分层 sensitivity
- 优化循环
- 目标函数封装
- 参数批量注入
- 与 `numpy/scipy` 或其它优化器联动

## fallback 顺序

1. 复用项目内已验证 runner
2. 直接 `.NET / Automation3`
3. Python 便利层
4. COM
5. GUI

## 不建议的误区

- 不要在没有验证 `.NET` 通道前，直接假设 Python 包一定能控住 DWSIM
- 不要把 `COM` 当默认方案
- 不要把 GUI 操作当主生产路径
- 不要因为 Python 层更“短”就跳过底层 smoke test

## 技能内的默认判断

如果同时存在多条可行路径，默认选择：

1. 已在当前项目中证明成功的 runner
2. 否则选择官方 `Automation3`
3. Python 便利层只在“确有价值且环境兼容”时启用
