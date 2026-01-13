using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace PopupV2NavigationWithParameters;

public partial class MessagePopup : Popup
{
	public MessagePopup(MessagePopupVm vm)
	{
		InitializeComponent();
        BindingContext = vm;
        Opened += MessagePopup_Opened;
	}

    private void MessagePopup_Opened(object? sender, EventArgs e)
    {       

    }
}

public record MessagePopupVmInitData(string Title, string Message);
public partial class MessagePopupVm(IPopupService popupService) : ObservableObject, IPopupViewModel<MessagePopupVmInitData>
{
    private readonly IPopupService popupService = popupService;
    [ObservableProperty] string _message = "Help... i'm empty :(", title = "No title :(";

    public void Init(MessagePopupVmInitData initData)
	{
        Title = initData.Title;
        Message = initData.Message;
	}

	[RelayCommand]
	private void Close() => popupService.ClosePopupAsync(Application.Current?.Windows[0]?.Page?.Navigation);
}