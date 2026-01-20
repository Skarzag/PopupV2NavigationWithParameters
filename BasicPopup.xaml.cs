using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace PopupV2NavigationWithParameters;

public partial class BasicPopup : Popup
{
	public BasicPopup(BasicPopupVm vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}

public partial class BasicPopupVm() : ObservableObject
{
	[RelayCommand]
	private static void Close() => Application.Current?.Windows[0]?.Page?.Navigation.ClosePopupAsync();
}