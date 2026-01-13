namespace PopupV2NavigationWithParameters;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new NavigationPage(IPlatformApplication.Current.Services.GetService<MainPage>()));
    }
}