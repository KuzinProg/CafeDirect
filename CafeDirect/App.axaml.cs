using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using CafeDirect.ViewModels;
using CafeDirect.Views;
using ReactiveUI;
using ReactiveUI.Samples.Suspension.Drivers;
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
        var suspension = new AutoSuspendHelper(ApplicationLifetime!);
        RxApp.SuspensionHost.CreateNewAppState = () => new MainWindowViewModel();
        RxApp.SuspensionHost.SetupDefaultSuspendResume(new NewtonsoftJsonSuspensionDriver("appstate.json"));
        suspension.OnFrameworkInitializationCompleted();

        Locator.CurrentMutable.RegisterConstant<IScreen>(RxApp.SuspensionHost.GetAppState<MainWindowViewModel>());
        Locator.CurrentMutable.Register<IViewFor<AuthControlViewModel>>(() => new AuthControlView());
        Locator.CurrentMutable.Register<IViewFor<RegistrationControlViewModel>>(() => new RegistrationControlView());
        Locator.CurrentMutable.Register<IViewFor<AdminControlViewModel>>(() => new AdminControlView());
        Locator.CurrentMutable.Register<IViewFor<CookControlViewModel>>(() => new CookControlView());
        Locator.CurrentMutable.Register<IViewFor<WaiterControlViewModel>>(() => new WaiterControlView());
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindowView { DataContext = Locator.Current.GetService<IScreen>()};
            desktop.MainWindow.Show();
        }

        //new MainWindowView { DataContext = Locator.Current.GetService<IScreen>()}.Show();
        base.OnFrameworkInitializationCompleted();
    }
}