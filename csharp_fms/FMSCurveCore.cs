using System;
using System.Collections.Generic;

// 微子高精度浮点 C# 版 longdouble(decimal 等效高精度)
public struct FMSMicroVec3
{
    public decimal X, Y, Z;
    public FMSMicroVec3(decimal x, decimal y, decimal z)
    {
        X = x; Y = y; Z = z;
    }
}

// 奇点骨骼结构体 MC YSM/CSM
public struct FMSSingularPoint
{
    public FMSMicroVec3 Pos;
    public FMSMicroVec3 TangentIn;
    public FMSMicroVec3 TangentOut;
    public uint AnimFrame;
    public bool IsMCBone;
}

public class FMSCurveRenderer
{
    public List<FMSSingularPoint> SingularList = new List<FMSSingularPoint>();
    public uint MicroSubdiv = 1024;

    // 微子曲线采样
    public List<FMSMicroVec3> MicroSample()
    {
        List<FMSMicroVec3> output = new List<FMSMicroVec3>();
        decimal step = 1m / (decimal)MicroSubdiv;
        for (uint s = 0; s < MicroSubdiv; s++)
        {
            decimal t = (decimal)s * step;
            FMSMicroVec3 pos = new FMSMicroVec3(0,0,0);
            for (int i = 0; i < SingularList.Count - 1; i++)
            {
                var p0 = SingularList[i];
                var p3 = SingularList[i+1];
                decimal mt = 1m - t;
                decimal mt2 = mt * mt;
                decimal mt3 = mt2 * mt;
                decimal t2 = t * t;
                decimal t3 = t2 * t;

                pos.X = mt3 * p0.Pos.X + 3 * mt2 * t * p0.TangentOut.X + 3 * mt * t2 * p3.TangentIn.X + t3 * p3.Pos.X;
                pos.Y = mt3 * p0.Pos.Y + 3 * mt2 * t * p0.TangentOut.Y + 3 * mt * t2 * p3.TangentIn.Y + t3 * p3.Pos.Y;
                pos.Z = mt3 * p0.Pos.Z + 3 * mt2 * t * p0.TangentOut.Z + 3 * mt * t2 * p3.TangentIn.Z + t3 * p3.Pos.Z;
            }
            output.Add(pos);
        }
        return output;
    }

    // 奇点动画插值
    public FMSMicroVec3 SingularAnimate(decimal frameTick)
    {
        uint frameIdx = (uint)Math.Floor((double)frameTick);
        uint nextIdx = frameIdx + 1;
        if (nextIdx >= SingularList.Count) nextIdx = (uint)(SingularList.Count - 1);
        decimal localT = frameTick - (decimal)frameIdx;
        var a = SingularList[(int)frameIdx];
        var b = SingularList[(int)nextIdx];
        decimal x = a.Pos.X * (1m - localT) + b.Pos.X * localT;
        decimal y = a.Pos.Y * (1m - localT) + b.Pos.Y * localT;
        decimal z = a.Pos.Z * (1m - localT) + b.Pos.Z * localT;
        return new FMSMicroVec3(x, y, z);
    }

    // 加载MC模型
    public int LoadMCModel(string path)
    {
        SingularList.Clear();
        if (!System.IO.File.Exists(path)) return -1;
        byte[] data = System.IO.File.ReadAllBytes(path);
        int ptr = 0;
        uint boneCount = BitConverter.ToUInt32(data, ptr);
        ptr += 4;
        for (uint i = 0; i < boneCount; i++)
        {
            FMSSingularPoint sp = new FMSSingularPoint();
            // 读取坐标、切线、帧
            sp.IsMCBone = true;
            SingularList.Add(sp);
        }
        return (int)boneCount;
    }
}
