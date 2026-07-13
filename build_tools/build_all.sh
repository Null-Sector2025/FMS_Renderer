#!/data/data/com.termux/files/usr/bin/bash
# FMS_Renderer 微子级曲线渲染引擎 全平台编译脚本
# 兼容 Termux 本地编译 / GitHub Actions CI
set -euo pipefail
ROOT=$(pwd)/../
OUT_DIR=${ROOT}/build_out
mkdir -p ${OUT_DIR}

echo "====================================="
echo "FMS Renderer Micro Precision Engine Build"
echo "Project Root: ${ROOT}"
echo "Output Dir: ${OUT_DIR}"
echo "Support: C++ Core / C# Editor / Java Android / Python Script"
echo "MC Model: YSM & CSM Bones Singular Animation"
echo "====================================="

# 1. Python 脚本库打包
echo -e "\n[Step 1/4] Build Python FMS Script Module"
cd ${ROOT}/python_fms
python3 -m pip install numpy --upgrade
python3 build.py
cp -r dist/* ${OUT_DIR}/python/

# 2. C++ 底层微子渲染内核 NDK 编译
echo -e "\n[Step 2/4] Build C++ FMS Core Render Library"
cd ${ROOT}/cpp_fms
cmake -B build -DCMAKE_TOOLCHAIN_FILE=${ROOT}/build_tools/ndk.toolchain.cmake
cmake --build build -j$(nproc)
cp build/libfms_core.a ${OUT_DIR}/cpp/

# 3. Java Android SDK 编译APK
echo -e "\n[Step 3/4] Build Java Android SDK & Demo APK"
cd ${ROOT}/java_fms
./gradlew assembleRelease
cp app/build/outputs/apk/release/*.apk ${OUT_DIR}/android/

# 4. C# MAUI 桌面/移动端编辑器
echo -e "\n[Step 4/4] Build C# FMS Editor"
cd ${ROOT}/csharp_fms
dotnet publish -c Release
cp -r bin/Release/* ${OUT_DIR}/csharp/

echo -e "\n✅ All platforms build complete! Output: ${OUT_DIR}"
