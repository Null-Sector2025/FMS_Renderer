# FMS C++ NDK arm64-v8a 微子高精度 longdouble
set(ANDROID_ABI arm64-v8a)
set(ANDROID_PLATFORM android-34)
set(CMAKE_ANDROID_ARCH_ABI arm64-v8a)
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_C_STANDARD 11)

# 全局功能宏
add_definitions(
    -DFMS_MICRO_PRECISION=1
    -DFMS_SINGULAR_ANIM=1
    -DFMS_MC_YSM_CSM=1
)

# 判断是否为Android交叉编译，区分ARM/x86编译参数
if(DEFINED CMAKE_TOOLCHAIN_FILE MATCHES "ndk")
    # NDK ARM64编译，启用Neon浮点优化
    set(CMAKE_CXX_FLAGS "-O3 -ffast-math -mfpmath=neon -march=armv8-a")
else()
    # x86 Linux CI本地编译，移除ARM专属参数
    set(CMAKE_CXX_FLAGS "-O3 -ffast-math")
endif()
set(CMAKE_CXX_FLAGS_RELEASE "-DNDEBUG ${CMAKE_CXX_FLAGS}")
