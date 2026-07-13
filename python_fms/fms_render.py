import numpy as np
import os

# 微子级高精度曲线渲染 + MC YSM/CSM骨骼奇点动画
class FMSMicroCurve:
    def __init__(self):
        self.singular_points = []
        self.micro_subdiv = 1024
        self.mc_model_path = ""

    class SingularPoint:
        def __init__(self, x, y, z, tan_in, tan_out, frame, is_mc_bone=False):
            self.x = np.longdouble(x)
            self.y = np.longdouble(y)
            self.z = np.longdouble(z)
            self.tangent_in = np.array(tan_in, dtype=np.longdouble)
            self.tangent_out = np.array(tan_out, dtype=np.longdouble)
            self.anim_frame = int(frame)
            self.is_mc_bone = is_mc_bone

    def add_singular(self, singular_obj):
        self.singular_points.append(singular_obj)

    def micro_sample_curve(self):
        """微子细分采样，返回高精度曲线顶点数组"""
        vertex_list = []
        step = np.longdouble(1.0 / self.micro_subdiv)
        for seg_idx in range(self.micro_subdiv):
            t = np.longdouble(seg_idx) * step
            pos = np.array([0.0, 0.0, 0.0], dtype=np.longdouble)

            for i in range(len(self.singular_points)-1):
                p0 = self.singular_points[i]
                p3 = self.singular_points[i+1]
                mt = 1.0 - t
                mt2 = mt * mt
                mt3 = mt2 * mt
                t2 = t * t
                t3 = t2 * t

                pos = (
                    mt3 * np.array([p0.x, p0.y, p0.z])
                    + 3 * mt2 * t * p0.tangent_out
                    + 3 * mt * t2 * p3.tangent_in
                    + t3 * np.array([p3.x, p3.y, p3.z])
                )
            vertex_list.append(pos)
        return np.array(vertex_list)

    def singular_animate_tick(self, frame_tick):
        """MC骨骼奇点动画插值"""
        frame_idx = int(np.floor(frame_tick))
        next_idx = frame_idx + 1
        if next_idx >= len(self.singular_points):
            next_idx = len(self.singular_points) - 1
        local_t = np.longdouble(frame_tick - frame_idx)
        p_a = self.singular_points[frame_idx]
        p_b = self.singular_points[next_idx]

        out_x = p_a.x * (1 - local_t) + p_b.x * local_t
        out_y = p_a.y * (1 - local_t) + p_b.y * local_t
        out_z = p_a.z * (1 - local_t) + p_b.z * local_t
        return np.array([out_x, out_y, out_z], dtype=np.longdouble)

    def load_mc_ysm_csm(self, model_file):
        """读取我的世界YSM/CSM骨骼模型文件"""
        self.mc_model_path = model_file
        self.singular_points.clear()
        with open(model_file, "rb") as f:
            bone_count = int.from_bytes(f.read(4), byteorder="little")
            for _ in range(bone_count):
                x = np.longdouble(f.read(8))
                y = np.longdouble(f.read(8))
                z = np.longdouble(f.read(8))
                tin = [np.longdouble(f.read(8)) for _ in range(3)]
                tout = [np.longdouble(f.read(8)) for _ in range(3)]
                frame = int.from_bytes(f.read(4), byteorder="little")
                sp = self.SingularPoint(x, y, z, tin, tout, frame, True)
                self.add_singular(sp)
        print(f"Loaded MC Bones: {bone_count}")
        return bone_count

# 测试入口
if __name__ == "__main__":
    engine = FMSMicroCurve()
    # 读取mc_models下模型示例
    test_model = os.path.join("..", "mc_models", "test_bone.ysm")
    if os.path.exists(test_model):
        engine.load_mc_ysm_csm(test_model)
        verts = engine.micro_sample_curve()
        print(f"Generated Micro Vertex Count: {len(verts)}")
