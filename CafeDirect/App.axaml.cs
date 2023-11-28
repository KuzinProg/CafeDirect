using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CafeDirect.ViewModels;
using CafeDirect.Views;
using ReactiveUI;
using Splat;

namespace CafeDirect;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        base.OnFrameworkInitializationCompleted();
        RxApp.SuspensionHost.CreateNewAppState = () => new MainWindowViewModel();
        Locator.CurrentMutable.RegisterConstant<IScreen>(RxApp.SuspensionHost.GetAppState<MainWindowViewModel>());
        Locator.CurrentMutable.Register<IViewFor<AuthControlViewModel>>(() => new AuthControlView());
        Locator.CurrentMutable.Register<IViewFor<RegistrationControlViewModel>>(() => new RegistrationControlView());
        new MainWindowView { DataContext = Locator.Current.GetService<IScreen>()}.Show();

    }
}