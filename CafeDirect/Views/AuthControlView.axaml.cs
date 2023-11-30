using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using CafeDirect.ViewModels;
using ReactiveUI;

namespace CafeDirect.Views
{

    public class AuthControlView : ReactiveUserControl<AuthControlViewModel>
    {
        public AuthControlView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}