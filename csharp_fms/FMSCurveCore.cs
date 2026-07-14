namespace FMSCurveEditor;

using System;

/// <summary>
/// 微子级FMS曲线渲染+奇点动画核心计算库
/// 适配我的世界 YSM/CSM骨骼模型形变
/// </summary>
public class FMSCurveCore
{
    // 微子级高精度曲线缓存
    private double[] _curveData;
    private double _singularityThreshold = 1e-12;

    public FMSCurveCore()
    {
        // 初始化曲线缓冲区
        _curveData = new double[1024];
    }

    /// <summary>
    /// FMS自由样条曲线细分计算
    /// </summary>
    public double CalculateCurvePoint(double t, double[] controlPoints)
    {
        double result = 0;
        for (int i = 0; i < controlPoints.Length; i++)
        {
            result += controlPoints[i] * SplineWeight(t, i, controlPoints.Length);
        }
        return result;
    }

    /// <summary>
    /// 奇点检测算法，识别动画突变点
    /// </summary>
    public bool DetectSingularity(double prev, double curr)
    {
        return Math.Abs(curr - prev) > _singularityThreshold;
    }

    private double SplineWeight(double t, int index, int total)
    {
        double x = t - (double)index / total;
        return Math.Exp(-x * x * 128);
    }
}
