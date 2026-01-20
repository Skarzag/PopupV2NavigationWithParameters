using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace PopupV2NavigationWithParameters;

public partial class PopupWithInit : Popup
{
    private readonly PopupWithInitInitData initData;

    public PopupWithInit(PopupWithInitVm vm, PopupWithInitInitData initData)
	{
		InitializeComponent();
        BindingContext = vm;
        Opened += MessagePopup_Opened;
        this.initData = initData;
    }

    private void MessagePopup_Opened(object? sender, EventArgs e)
    {
        Opened -= MessagePopup_Opened;

        if (BindingContext is PopupWithInitVm vm)
            vm.Init(initData);
    }
}

public record PopupWithInitInitData(string Title, string Message);

public partial class PopupWithInitVm() : ObservableObject
{
    [ObservableProperty] string _message = "Help... i'm empty :(", title = "No title :(";

    public void Init(PopupWithInitInitData initData)
    {
        Title = initData.Title;
        Message = initData.Message;
    }

    [RelayCommand]
    private static void Close() => Application.Current?.Windows[0]?.Page?.Navigation.ClosePopupAsync();
}