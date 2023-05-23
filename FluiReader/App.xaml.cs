namespace FluiReader;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
    public static IServiceProvider Services =>
#if ANDROID
        MauiApplication.Current.Services;
#elif WINDOWS10_0_17763_0_OR_GREATER
        MauiWinUIApplication.Current.Services;
#elif MACCATALYST || IOS
        MauiUIApplicationDelegate.Current.Services;
#else
        null;
#endif
}
