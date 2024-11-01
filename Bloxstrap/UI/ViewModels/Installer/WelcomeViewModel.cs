namespace Bloxstrap.UI.ViewModels.Installer
{
    public class WelcomeViewModel : NotifyPropertyChangedViewModel
    {
        // formatting is done here instead of in xaml, it's just a bit easier
        public string MainText => String.Format(
            Strings.Installer_Welcome_MainText,
            "[https://github.com/ZFkQSZ3GxWFy6bthZ/_](https://github.com/ZFkQSZ3GxWFy6bthZ/_)"
        );

        public bool CanContinue { get; set; } = false;
    }
}
