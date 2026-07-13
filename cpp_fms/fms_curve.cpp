#include "fms_curve.h"
#include <fstream>
#include <cmath>
#include <iostream>

std::vector<std::vector<fms_micro_t>> FMS_CurveRenderer::MicroSample(uint32_t subdiv) {
    std::vector<std::vector<fms_micro_t>> vertex_output;
    const fms_micro_t step = 1.0L / static_cast<fms_micro_t>(subdiv);

    for (uint32_t s = 0; s < subdiv; s++) {
        const fms_micro_t t = static_cast<fms_micro_t>(s) * step;
        std::vector<fms_micro_t> pos(3, 0.0L);

        for (size_t i = 0; i < singular_list.size() - 1; i++) {
            const auto& p0 = singular_list[i];
            const auto& p3 = singular_list[i+1];

            const fms_micro_t mt = 1.0L - t;
            const fms_micro_t mt2 = mt * mt;
            const fms_micro_t mt3 = mt2 * mt;
            const fms_micro_t t2 = t * t;
            const fms_micro_t t3 = t2 * t;

            // 三阶贝塞尔微子高精度坐标计算
            pos[0] = mt3 * p0.x
                   + 3 * mt2 * t * p0.tangent_out[0]
                   + 3 * mt * t2 * p3.tangent_in[0]
                   + t3 * p3.x;

            pos[1] = mt3 * p0.y
                   + 3 * mt2 * t * p0.tangent_out[1]
                   + 3 * mt * t2 * p3.tangent_in[1]
                   + t3 * p3.y;

            pos[2] = mt3 * p0.z
                   + 3 * mt2 * t * p0.tangent_out[2]
                   + 3 * mt * t2 * p3.tangent_in[2]
                   + t3 * p3.z;
        }
        vertex_output.push_back(pos);
    }
    return vertex_output;
}

void FMS_CurveRenderer::SingularAnimate(fms_micro_t frame_tick, fms_micro_t out_vert[3]) {
    uint32_t frame_idx = static_cast<uint32_t>(std::floor(frame_tick));
    uint32_t next_idx = frame_idx + 1;
    if (next_idx >= singular_list.size()) next_idx = singular_list.size() - 1;

    const fms_micro_t local_t = frame_tick - static_cast<fms_micro_t>(frame_idx);
    const auto& p_a = singular_list[frame_idx];
    const auto& p_b = singular_list[next_idx];

    // 奇点线性平滑插值动画
    out_vert[0] = p_a.x * (1.0L - local_t) + p_b.x * local_t;
    out_vert[1] = p_a.y * (1.0L - local_t) + p_b.y * local_t;
    out_vert[2] = p_a.z * (1.0L - local_t) + p_b.z * local_t;
}

int FMS_CurveRenderer::LoadMCModelCurve(const std::string& model_path) {
    std::ifstream file(model_path, std::ios::binary);
    if (!file.is_open()) {
        std::cerr << "[FMS ERROR] Can not open MC model: " << model_path << std::endl;
        return -1;
    }

    uint32_t bone_count = 0;
    file.read(reinterpret_cast<char*>(&bone_count), sizeof(uint32_t));
    singular_list.resize(bone_count);

    for (uint32_t i = 0; i < bone_count; i++) {
        auto& sp = singular_list[i];
        file.read(reinterpret_cast<char*>(&sp.x), sizeof(fms_micro_t) * 3);
        file.read(reinterpret_cast<char*>(&sp.tangent_in), sizeof(fms_micro_t) * 3);
        file.read(reinterpret_cast<char*>(&sp.tangent_out), sizeof(fms_micro_t) * 3);
        file.read(reinterpret_cast<char*>(&sp.anim_frame), sizeof(uint32_t));
        sp.is_mc_bone = true;
    }
    file.close();
    return static_cast<int>(bone_count);
}
