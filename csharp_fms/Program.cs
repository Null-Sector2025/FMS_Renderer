using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace FMSCurveEditor;

// 标准MAUI9入口，继承MauiApplication（非密封类）
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

// 顶层App仅指定首页，无需重写CreateWindow
public class App : MauiApplication
{
    public App()
    {
        MainPage = new MainPage();
    }
}

// 主渲染编辑器页面
public class MainPage : ContentPage
{
    public MainPage()
    {
        Title = "FMS 微子级曲线奇点渲染编辑器";
        BackgroundColor = Color.FromArgb("#1E1E1E");
        Content = new Label
        {
            Text = "FMS Curve Render Engine\n适配我的世界 Java/基岩/网易 YSM/CSM骨骼模型",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 18,
            TextColor = Colors.White
        };
    }
}
