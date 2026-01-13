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
    private async Task ShowPopup()
    {
        await popupMauiService.ShowPopupAsyncV1<MessagePopupVm, MessagePopupVmInitData>(new MessagePopupVmInitData("A title", "A message"));
    }

    [RelayCommand]
    private async Task ShowPopupV2()
    {
        await popupMauiService.ShowPopupAsyncV2<MessagePopupV2, MessagePopupVmInitData>(new MessagePopupVmInitData("A title", "A message"));
    }
}