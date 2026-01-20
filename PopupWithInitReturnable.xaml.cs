using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace PopupV2NavigationWithParameters;

public partial class PopupWithInitReturnable : Popup<PopupWithInitReturnable_ReturnData>
{
    private readonly PopupWithInitReturnable_InitData initData;

    public PopupWithInitReturnable(PopupWithInitReturnableVm vm, PopupWithInitReturnable_InitData initData)
	{
		InitializeComponent();
        BindingContext = vm;
        Opened += MessagePopup_Opened;
        this.initData = initData;
    }

    private void MessagePopup_Opened(object? sender, EventArgs e)
    {
        if (BindingContext is PopupWithInitReturnableVm vm)
            vm.Init(initData);
    }
}

public record PopupWithInitReturnable_InitData(string Title, string Message);
public record PopupWithInitReturnable_ReturnData(string ReturnMessage);

public partial class PopupWithInitReturnableVm(IPopupService popupService) : ObservableObject
{
    private readonly IPopupService popupService = popupService;
    [ObservableProperty] string _message = "Help... i'm empty :(", title = "No title :(";

    public void Init(PopupWithInitReturnable_InitData initData)
    {
        Title = initData.Title;
        Message = initData.Message;
    }

    [RelayCommand]
    private static void NegativeAnswer() => Application.Current?.Windows[0]?.Page?.Navigation.ClosePopupAsync<PopupWithInitReturnable_ReturnData>(new("Negative"));
    [RelayCommand]
    private static void PositiveAnwer() => Application.Current?.Windows[0]?.Page?.Navigation.ClosePopupAsync<PopupWithInitReturnable_ReturnData>(new("Positive"));
}