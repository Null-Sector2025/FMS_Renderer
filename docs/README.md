# FMS_Renderer 微子级曲线奇点渲染引擎
## 项目简介
自研FMS三阶贝塞尔曲线渲染 + 奇点骨骼动画系统，全平台多语言实现：
- C++：底层高精度内核，longdouble微子浮点，NDK跨平台编译
- Python：Termux脚本工具，批量处理MC YSM/CSM模型
- C#：MAUI 游戏模型编辑器，可视化曲线调整
- Java：Android APP 渲染封装，对接C++动态库

## 我的世界支持
兼容 MC Java版 / 基岩版 / 网易版 YSM、CSM骨骼模型
奇点系统专门适配MC生物骨骼动画拐点、形变曲线渲染。

## 编译方式
1. Termux本地编译
cd ~/FMS_Renderer
./build_tools/build_all.sh

2. GitHub Actions CI编译
根目录内置.github/workflows自动编译脚本（可自行添加）

## 目录结构
- build_tools：全平台编译脚本、NDK工具链
- cpp_fms：底层渲染核心静态库
- csharp_fms：MAUI可视化编辑器
- java_fms：Android SDK与APK工程
- python_fms：脚本处理工具链
- mc_models：MC YSM/CSM模型素材软链接目录
- build_out：所有平台编译产物输出目录
