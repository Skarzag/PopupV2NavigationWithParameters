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

        builder.Services.AddTransientPopup<MessagePopup, MessagePopupVm>();
        builder.Services.AddTransient<Func<MessagePopupVmInitData, MessagePopupV2>>(_ => initData => new MessagePopupV2(initData)); 
        
        return builder.Build();
    }
}
