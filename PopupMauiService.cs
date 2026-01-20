using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;

namespace PopupV2NavigationWithParameters;

public interface IPopupMauiService
{
    Task ShowPopupAsync<TView>(INavigation nav, bool closePreviousPopup = false)
    where TView : Popup;

    Task ShowPopupAsync<TView, TInit>(INavigation nav, TInit initData, bool closePreviousPopup = false)
        where TView : Popup
        where TInit : notnull;

    Task<TReturn?> ShowPopupAsync<TView, TReturn>(INavigation nav, bool closePreviousPopup = false)
        where TView : Popup<TReturn?>;

    Task<TReturn?> ShowPopupAsync<TView, TInit, TReturn>(INavigation nav, TInit initData, bool closePreviousPopup = false)
            where TView : Popup<TReturn?>
            where TInit : notnull;
}

internal class PopupMauiService(IPopupService popupService) : IPopupMauiService
{
    private readonly IPopupService _popupService = popupService;

    public async Task ShowPopupAsync<TView>(INavigation nav, bool forceClosePreviousPopup = false) 
        where TView : Popup
    {
        nav ??= Application.Current?.Windows[0].Page?.Navigation ?? throw new NullReferenceException($"No Navigation available to show popup {typeof(TView)}");

        if (forceClosePreviousPopup)
            await _popupService.ClosePopupAsync(nav);

        var laPopup = IPlatformApplication.Current?.Services.GetService<TView>() ?? throw new NullReferenceException($"Error while instantiating a popup of type {typeof(TView)}");

        await nav.ShowPopupAsync<TView>(laPopup);
    }

    public async Task ShowPopupAsync<TView, TInit>(INavigation nav, TInit initData, bool forceClosePreviousPopup = false)
        where TView : Popup
        where TInit : notnull
    {
        nav ??= Application.Current?.Windows[0].Page?.Navigation ?? throw new NullReferenceException($"No Navigation available to show popup {typeof(TView)}");

        if (forceClosePreviousPopup)
            await _popupService.ClosePopupAsync(nav);

        var laPopup = IPlatformApplication.Current?.Services?.GetRequiredService<Func<TInit, TView>>()(initData) ?? throw new NullReferenceException($"Error while instantiating a popup of type {typeof(TView)}");

        await nav.ShowPopupAsync<TView>(laPopup);
    }

    public async Task<TReturn?> ShowPopupAsync<TView, TReturn>(INavigation nav, bool forceClosePreviousPopup = false)
        where TView : Popup<TReturn?>
    { 
        nav ??= Application.Current?.Windows[0].Page?.Navigation ?? throw new NullReferenceException($"No Navigation available to show popup {typeof(TView)}");

        if (forceClosePreviousPopup)
            await _popupService.ClosePopupAsync(nav);

        var laPopup = IPlatformApplication.Current?.Services.GetService<TView>() ?? throw new NullReferenceException($"Error while instantiating a popup of type {typeof(TView)}");

        var result = await nav.ShowPopupAsync<TReturn>(laPopup);

        return result.Result;
    }

    public async Task<TReturn?> ShowPopupAsync<TView, TInit, TReturn>(INavigation nav, TInit initData, bool forceClosePreviousPopup = false)
        where TView : Popup<TReturn?>
        where TInit : notnull
    {
        nav ??= Application.Current?.Windows[0].Page?.Navigation ?? throw new NullReferenceException($"No Navigation available to show popup {typeof(TView)}");

        if (forceClosePreviousPopup)
            await _popupService.ClosePopupAsync(nav);

        var laPopup = IPlatformApplication.Current?.Services?.GetRequiredService<Func<TInit, TView>>()(initData) ?? throw new NullReferenceException($"Error while instantiating a popup of type {typeof(TView)}");

        var result = await nav.ShowPopupAsync<TReturn>(laPopup);

        return result.Result;
    }
}