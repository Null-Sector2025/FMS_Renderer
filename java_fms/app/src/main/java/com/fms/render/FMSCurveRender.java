package com.fms.render;
import java.util.ArrayList;
import java.io.RandomAccessFile;

// Java 高精度微子曲线渲染，对接C++ libfms_core.so
public class FMSCurveRender {
    public static class MicroVec3 {
        public double x, y, z;
        public MicroVec3(double x, double y, double z) {
            this.x = x; this.y = y; this.z = z;
        }
    }

    public static class SingularPoint {
        public MicroVec3 pos;
        public MicroVec3 tanIn, tanOut;
        public int animFrame;
        public boolean isMCBone;
    }

    private ArrayList<SingularPoint> singularList = new ArrayList<>();
    private int microSubdiv = 1024;

    // 本地方法 调用C++底层库
    public native ArrayList<MicroVec3> nativeMicroSample();
    public native MicroVec3 nativeSingularAnimate(double tick);
    public native int nativeLoadMCModel(String path);

    // Java层封装调用
    public ArrayList<MicroVec3> getMicroCurve() {
        return nativeMicroSample();
    }

    public MicroVec3 getAnimVertex(double frameTick) {
        return nativeSingularAnimate(frameTick);
    }

    public int loadYsmCsmModel(String filePath) {
        return nativeLoadMCModel(filePath);
    }

    // 加载C++底层so库
    static {
        System.loadLibrary("fms_core");
    }
}
