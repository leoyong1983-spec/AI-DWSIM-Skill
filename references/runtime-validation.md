# 可执行验证 / Executable Validation

## 用途 / Purpose

`scripts/run_dwsim.ps1` 提供 Windows x64 的 Automation3 执行入口。无模型时建立纯水混合器示例；指定模型时先复制到新目录，再计算、保存、重新加载并重算。无需安装 `pythonnet`、优化器或 MCP 服务。已有项目 runner 优先。

The Windows x64 runner creates a water-mixer demonstration when no model is supplied, or copies and recalculates a supplied model. Both modes save, reload, recalculate, and export evidence. It uses the local .NET Framework compiler and DWSIM installation; no Python integration package is required. Prefer an existing proven project runner when available.

## 命令 / Commands

从技能目录运行，将安装路径改为本机实际路径，输出目录每次必须是新目录。环境演示的参数不可当成项目工况。

Run from the skill directory, substitute the actual installation path, and use a new output directory for every run. Demonstration inputs are not a project design basis.

```powershell
.\scripts\run_dwsim.ps1 `
  -DwsimDirectory 'C:\Program Files\DWSIM' `
  -OutputDirectory '.\AGENTCODING\05_qa\water-mixer-01'

.\scripts\run_dwsim.ps1 `
  -DwsimDirectory 'C:\Program Files\DWSIM' `
  -ModelPath '.\approved-model.dwxmz' `
  -OutputDirectory '.\AGENTCODING\05_qa\model-review-01' `
  -TimeoutSeconds 120
```

模型必须来自可信来源：加载或计算可能执行模型内脚本或插件。此入口复制单个 `.dwxmz` / `.dwxml` 文件；含外部数据、子流程、CAPE-OPEN、定制插件或动态工况的模型，应使用项目专用 runner 保留其依赖和运行约束。

Load only trusted models: loading or calculation can execute embedded scripts or plugins. This runner copies a single `.dwxmz` / `.dwxml` file. Use a project runner for external data, subflowsheets, CAPE-OPEN objects, custom plugins, or dynamic cases. The worker process provides timeout isolation, not a security sandbox.

## 成功条件 / Success Criteria

- 真实求解：先重置计算状态，检查 `CalculateFlowsheet4` 返回的错误列表与 `Solved`。DWSIM 9.0.5 的公开 `CalculateFlowsheet2` 返回 `void`，不能将其空返回值当成成功。
- 结果检查：拒绝非有限数值、非正绝对温度/压力、负流量和无物料流股的模型。
- 往返检查：保存再加载后，对象 ID/类型和流股数量保持一致，温度、压力、流量、比焓差值不超过 `1e-6 * max(1, abs(original))`。
- 示例衡算：纯水、IAPWS-IF97、绝热混合器；进料为 300 K / 1 kg/s 和 340 K / 2 kg/s，均为 101325 Pa。物料残差限值 `1e-6 kg/s`，能量残差限值 `1e-3 kW`。
- 原件保护：已有模型只读取和复制，记录运行前后 SHA-256；输出目录已存在时拒绝运行。

Success requires a fresh calculation with no returned solver errors, `Solved=true`, finite and physically admissible stream scalars, and consistent inventory/results after save/reload. The water example additionally checks material and energy balances with the limits above. Source hashes must remain unchanged. Existing output directories are rejected.

普通模型的衡算容差、组分/元素衡算、设备约束、目标 KPI 和工艺合理性需按项目补充；当前入口不替代这些工程审查。水混合器演示只证明执行路径，不能完成氢、氨、甲醇等具体工艺的建模复核。

For supplied models, project-specific balance tolerances, composition/element balances, equipment constraints, KPI targets, and process suitability still require engineering checks. A successful water demonstration proves the execution path only, not a hydrogen, ammonia, methanol, or other project model.

## 证据 / Evidence

`run-status.json` 包含版本及 DLL 哈希、每次求解的状态/错误/流股/对象清单、重算比较、清理警告和模型哈希。`key_streams.csv` 标明单位；`workcopy.dwxmz` 可重新加载；`compile.log`、`stdout.log`、`stderr.log` 用于故障定位。日志与模型可能包含项目数据，仅保存在指定输出目录，不自动上传。

The JSON records version and assembly hash, per-pass solver state/errors/streams/inventory, roundtrip comparison, cleanup warnings, and model hashes. The CSV uses explicit units; the native workcopy can be reloaded. Logs and generated models remain local and are not uploaded automatically.

退出码 `0` 表示这些程序检查通过，非零表示失败。编译/启动/超时与加载/求解/导出失败分开记录。前置路径检查失败会直接报错，且不创建输出目录。`engineering_acceptance: not_assessed` 表示仍需工程师验收。超时仅停止本次启动的 worker，不关闭已打开的 DWSIM。

Exit code zero means these software checks passed; nonzero means failure. Reports distinguish compilation/startup/timeout from load/solve/export failures. Invalid preflight paths fail before an output directory is created. Engineering acceptance remains `not_assessed`. Timeout stops only this worker, not an existing DWSIM session.

## 验证边界与来源 / Validation Boundary and Sources

本入口已针对本机 DWSIM 9.0.5 执行水混合器建模与保存重算验证；其他版本必须重新 smoke test。CI 的 fake-object tests 只验证报错和验收逻辑，不宣称在 GitHub runner 上运行 DWSIM。

Locally exercised with DWSIM 9.0.5 using the water mixer and save/reload recalculation. Other versions require a new smoke test. CI fake-object checks test evidence gates, not the DWSIM runtime.

- [DWSIM 9.0.5 Automation3 implementation](https://github.com/DanWBR/dwsim/blob/v9.0.5/DWSIM.Automation/Automation.cs)
- [Official automation examples](https://github.com/DanWBR/dwsim/tree/v9.0.5/DWSIM.Automation.Tests.CSharp)
- [MaterialStream.GetMassEnthalpy: kJ/kg](https://dwsim.org/api_help/html/M_DWSIM_Thermodynamics_Streams_MaterialStream_GetMassEnthalpy.htm)
