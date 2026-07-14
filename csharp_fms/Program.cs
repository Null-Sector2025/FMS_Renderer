using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;

namespace FMSCurveEditor;

#if ANDROID
// Android平台专用MAUI入口
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });
        return builder.Build();
    }
}

public class App : MauiApplication
{
    public App()
    {
        MainPage = new MainPage();
    }
}
#endif

// Windows桌面平台纯代码入口
namespace FMSCurveEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FMS Curve Render Editor Windows Desktop");
            Console.WriteLine("微子级曲线奇点渲染引擎 - Windows桌面端");
            // 基础业务初始化
            var core = new FMSCurveCore();
            Console.ReadLine();
        }
    }

    public class MainPage
    {
        // Windows无MAUI窗口，仅做数据载体
        public string Title { get; set; } = "FMS渲染编辑器";
    }
}
