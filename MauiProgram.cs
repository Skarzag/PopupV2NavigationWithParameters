using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Services;
using Microsoft.Extensions.Logging;

namespace PopupV2NavigationWithParameters;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<IPopupService, PopupService>();
        builder.Services.AddSingleton<IPopupMauiService, PopupMauiService>();
        builder.Services.AddTransient<MainPage, MainPageVm>();

        builder.Services.AddTransientPopup<BasicPopup, BasicPopupVm>();

        builder.Services.AddTransient<Func<PopupWithInitInitData, PopupWithInit>>(_ => initData => new PopupWithInit(_.GetRequiredService<PopupWithInitVm>(), initData));
        builder.Services.AddTransient<PopupWithInitVm>();

        builder.Services.AddTransient<Func<PopupWithInitReturnable_InitData, PopupWithInitReturnable>>(_ => initData => new PopupWithInitReturnable(_.GetRequiredService<PopupWithInitReturnableVm>(), initData));
        builder.Services.AddTransient<PopupWithInitReturnableVm>();

        return builder.Build();
    }
}
