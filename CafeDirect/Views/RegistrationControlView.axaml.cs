using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using CafeDirect.ViewModels;
using ReactiveUI;

namespace CafeDirect.Views
{

    public class RegistrationControlView : ReactiveUserControl<RegistrationControlViewModel>
    {
        public RegistrationControlView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}