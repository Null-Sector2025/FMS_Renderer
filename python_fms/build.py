import os
import shutil
import subprocess

# Python FMS 库打包脚本
dist_dir = "dist"
if os.path.exists(dist_dir):
    shutil.rmtree(dist_dir)
os.mkdir(dist_dir)

# 复制核心渲染文件
shutil.copy("fms_render.py", dist_dir)
# 生成依赖清单
with open(os.path.join(dist_dir, "requirements.txt"), "w") as f:
    f.write("numpy>=1.26.0\n")

print("Python FMS Script build finished, output: ./dist")
