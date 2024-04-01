using DotNetCW.Data;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace DotNetCW
{
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

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.VisibleStateDuration = 3000;
                config.SnackbarConfiguration.HideTransitionDuration = 200;
                config.SnackbarConfiguration.ShowTransitionDuration = 200;
                config.SnackbarConfiguration.MaxDisplayedSnackbars = 6;
            });

            builder.Services.AddSingleton<UserServices>();
            builder.Services.AddSingleton<MemberServices>();
            builder.Services.AddSingleton<OrderServices>();
            builder.Services.AddSingleton<ItemServices>();
            builder.Services.AddSingleton<PdfDataService>();

            return builder.Build();
        }
    }
}
