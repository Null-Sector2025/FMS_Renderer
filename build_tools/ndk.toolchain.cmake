# FMS C++ NDK arm64-v8a 工具链 微子级 longdouble 高精度
set(ANDROID_ABI arm64-v8a)
set(ANDROID_PLATFORM android-34)
set(CMAKE_ANDROID_ARCH_ABI arm64-v8a)
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_C_STANDARD 11)

# 开启微子浮点、奇点动画、MC模型支持宏定义
add_definitions(
    -DFMS_MICRO_PRECISION=1
    -DFMS_SINGULAR_ANIM=1
    -DFMS_MC_YSM_CSM=1
)

# 分架构编译参数：ARM启用Neon，x86 CI移除ARM专属flag
if(CMAKE_ANDROID_ARCH_ABI STREQUAL "arm64-v8a")
    set(CMAKE_CXX_FLAGS "-O3 -ffast-math -mfpmath=neon -march=armv8-a")
else()
    set(CMAKE_CXX_FLAGS "-O3 -ffast-math")
endif()
set(CMAKE_CXX_FLAGS_RELEASE "-DNDEBUG ${CMAKE_CXX_FLAGS}")
