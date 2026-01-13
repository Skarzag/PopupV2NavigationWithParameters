using CommunityToolkit.Maui.Views;
namespace PopupV2NavigationWithParameters;

public partial class MessagePopupV2 : Popup, IPopup<MessagePopupVmInitData>
{
    private readonly MessagePopupVmInitData initData;

    public MessagePopupV2(MessagePopupVmInitData initData)
	{
		InitializeComponent();
        BindingContext = IPlatformApplication.Current.Services.GetService<MessagePopupVm>();
        Opened += MessagePopup_Opened;
        this.initData = initData;
    }

    private void MessagePopup_Opened(object? sender, EventArgs e)
    {
        if (BindingContext is MessagePopupVm vm)
            vm.Init(initData);
    }
}