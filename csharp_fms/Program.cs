using Microsoft.Maui;
using Microsoft.Maui.Hosting;

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
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new MainPage());
    }
}

public class MainPage : ContentPage
{
    public MainPage()
    {
        Title = "FMS 曲线奇点渲染编辑器";
        Content = new Label
        {
            Text = "FMS Micro Curve Render Engine Editor\nMC YSM/CSM 模型曲线可视化工具",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
    }
}
