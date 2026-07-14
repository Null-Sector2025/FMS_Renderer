using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace FMSCurveEditor;

class Program : MauiApplication
{
    protected override MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
        return builder.Build();
    }

    static void Main(string[] args)
    {
        var app = new Program();
        app.Run(args);
    }
}

public class App : MauiApp
{
    // 修正MAUI9标准重写签名
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new MainPage());
    }
}

public class MainPage : ContentPage
{
    public MainPage()
    {
        Title = "FMS 微子级曲线奇点渲染编辑器";
        Content = new Label
        {
            Text = "FMS Micro Curve Render Engine\n适配我的世界 Java/基岩/网易 YSM/CSM骨骼模型",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 16,
            TextColor = Colors.White
        };
        BackgroundColor = Color.FromArgb("#222222");
    }
}
