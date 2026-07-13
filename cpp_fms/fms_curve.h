#ifndef FMS_CURVE_RENDER_H
#define FMS_CURVE_RENDER_H
#include <vector>
#include <cstdint>
#include <string>

// 微子级高精度浮点 80bit longdouble
using fms_micro_t = long double;

// 奇点结构体：MC骨骼拐点、动画突变控制点
struct FMS_SingularPoint {
    fms_micro_t x, y, z;
    fms_micro_t tangent_in[3];
    fms_micro_t tangent_out[3];
    uint32_t anim_frame;
    bool is_mc_bone;
};

// FMS 三阶贝塞尔曲线渲染器
class FMS_CurveRenderer {
public:
    std::vector<FMS_SingularPoint> singular_list;

    // 微子细分采样，subdiv最大1024段无锯齿曲线
    std::vector<std::vector<fms_micro_t>> MicroSample(uint32_t subdiv = 1024);

    // 奇点骨骼动画插值，输出当前帧顶点坐标
    void SingularAnimate(fms_micro_t frame_tick, fms_micro_t out_vert[3]);

    // 加载我的世界 YSM / CSM 骨骼模型文件
    int LoadMCModelCurve(const std::string& model_path);
};
#endif
