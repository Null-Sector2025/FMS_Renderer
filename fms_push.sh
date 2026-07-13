#!/data/data/com.termux/files/usr/bin/bash
# Null-Sector2025/FMS_Renderer 一键推送脚本
set -e
ROOT=$(pwd)
echo "===== FMS_Renderer 代码推送工具 | Repo: Null-Sector2025/FMS_Renderer ====="
cd ${ROOT}
# 查看变更文件
git status
# 添加全部修改
git add .
# 读取提交备注
read -p "输入本次提交说明：" commit_msg
git commit -m "${commit_msg}"
# 推送到main分支，自动触发CI编译
git push origin main
echo -e "\n✅ 推送完成，访问仓库查看CI自动编译：https://github.com/Null-Sector2025/FMS_Renderer/actions"
