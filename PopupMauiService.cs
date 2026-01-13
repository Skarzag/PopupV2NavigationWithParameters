using CommunityToolkit.Maui;
using System.ComponentModel;

namespace PopupV2NavigationWithParameters;


public interface IPopup<TInit>
    where TInit : notnull
{
}

public interface IPopupViewModel<TInit> : INotifyPropertyChanged, INotifyPropertyChanging
    where TInit : notnull
{
    void Init(TInit initData);
}

public interface IPopupMauiService
{
    Task ShowPopupAsyncV1<TViewModel, TInit>(TInit initData, bool forceClosePreviousPopup = false)
        where TViewModel : IPopupViewModel<TInit>
        where TInit : notnull;

    Task ShowPopupAsyncV2<TView, TInit>(TInit initData, bool forceClosePreviousPopup = false)
    where TView : IPopup<TInit>
    where TInit : notnull;
}

internal class PopupMauiService(IPopupService popupService) : IPopupMauiService
{
    private readonly IPopupService _popupService = popupService;

    //in this example, i'm just replicating the V1 style... but don't know how to pass data
    public async Task ShowPopupAsyncV1<TViewModel, TInit>(TInit initData, bool forceClosePreviousPopup)
        where TViewModel : IPopupViewModel<TInit>
        where TInit : notnull
    {
        var navigation = Application.Current?.Windows[0].Page?.Navigation ?? throw new Exception();

        if (forceClosePreviousPopup)
            await _popupService.ClosePopupAsync(navigation);

        await _popupService.ShowPopupAsync<TViewModel>(navigation);

        //WHAT WE NEED IS 
        //await _popupService.ShowPopupAsync<TViewModel>(navigation, onOpened: vm => vm.Init(initData));
        //or
        //await _popupService.ShowPopupAsync<TViewModel>(navigation, initData);
    }

    //in this example i thought that i can pass an data initiated popup to pass the data in the "famous" Opened event
    //but we can't pass popup directly to ShowPopupAsync... i've hallucinated i guess 😅
    //this was my last attempt to do this, sadly... i'm blocked
    public async Task ShowPopupAsyncV2<TView, TInit>(TInit initData, bool forceClosePreviousPopup)
    where TView : IPopup<TInit>
    where TInit : notnull
    {
        var navigation = Application.Current?.Windows[0].Page?.Navigation ?? throw new Exception();

        if (forceClosePreviousPopup)
            await _popupService.ClosePopupAsync(navigation);

        // Create a popup with initiated data
        var laPopup = IPlatformApplication.Current.Services.GetService<Func<TInit, TView>>()(initData);

        //_popupService.ShowPopupAsync(navigation, laPopup);
    }
}