using FluiReader.Services;
using FluiReader.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace FluiReader;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("FluentSystemIcons-Regular.ttf", "FluentSystemIconsRegular")
                .AddFont("FluentSystemIcons-Filled.ttf", "FluentSystemIconsFilled");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<LocaleService>();

        builder.Services.AddHttpClient()
            .AddScoped<ISubscriptionRepoService, LocalSubscriptionRepoService>()
            .AddSingleton<LocalDatabaseService>()
            .AddScoped<FeedService>();
        return builder.Build();
    }
}
