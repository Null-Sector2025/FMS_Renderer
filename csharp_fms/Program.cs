namespace FMSCurveEditor;

#if ANDROID
using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

// 标准MAUI9 正确入口，不单独引用MauiApp类型
public static class MauiProgram
{
    public static MauiApplication CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
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

public class MainPage : ContentPage
{
    public MainPage()
    {
        Title = "FMS 微子级曲线奇点渲染编辑器";
        BackgroundColor = Color.FromArgb("#1E1E1E");
        Content = new Label
        {
            Text = "FMS Curve Render Engine\n我的世界 YSM/CSM 骨骼曲线可视化",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 18,
            TextColor = Colors.White
        };
    }
}
#endif

#if WINDOWS
using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== FMS Curve Render Editor Windows ===");
        Console.WriteLine("底层核心：FMSCurveCore 曲线计算模块");
        FMSCurveCore core = new FMSCurveCore();
        Console.WriteLine("引擎初始化完成");
        Console.ReadLine();
    }
}

public class MainPage
{
    public string Title { get; set; } = string.Empty;
}
#endif
