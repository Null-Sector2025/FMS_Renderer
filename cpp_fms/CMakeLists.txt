cmake_minimum_required(VERSION 3.22)
project(FMS_Core LANGUAGES C CXX)
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_BUILD_TYPE Release)

# 全局功能宏
add_definitions(
    -DFMS_MICRO_PRECISION=1
    -DFMS_SINGULAR_ANIM=1
    -DFMS_MC_YSM_CSM=1
)

# 收集全部源码文件
file(GLOB SOURCE_FILES *.cpp *.h)
add_library(fms_core STATIC ${SOURCE_FILES})

# 编译产物输出到全局build_out目录
install(TARGETS fms_core DESTINATION ${CMAKE_SOURCE_DIR}/../build_out/cpp)
