using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PopupV2NavigationWithParameters;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


public partial class MainPageVm(IPopupMauiService popupMauiService) : ObservableObject
{
    private readonly IPopupMauiService popupMauiService = popupMauiService;

    [RelayCommand]
    private async Task ShowBasicPopup() 
    {
        await popupMauiService.ShowPopupAsync<BasicPopup>(Application.Current?.Windows[0].Page?.Navigation);
    }

    [RelayCommand]
    private async Task ShowPopupWithInitData()
    {
        await popupMauiService.ShowPopupAsync<PopupWithInit, PopupWithInitInitData>(Application.Current?.Windows[0].Page?.Navigation, new PopupWithInitInitData("InitatedTitle", "InitiatedMessage"));
    }

    [RelayCommand]
    private async Task ShowPopupWithInitDataReturnable()
    {
        var returnedData = await popupMauiService.ShowPopupAsync<PopupWithInitReturnable, PopupWithInitReturnable_InitData, PopupWithInitReturnable_ReturnData>(Application.Current?.Windows[0].Page?.Navigation, new PopupWithInitReturnable_InitData("InitatedTitle", "InitiatedMessage"));
        await popupMauiService.ShowPopupAsync<PopupWithInit, PopupWithInitInitData>(Application.Current?.Windows[0].Page?.Navigation, new PopupWithInitInitData("Returned Data", returnedData?.ReturnMessage));
    }
}