using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using CafeDirect.ViewModels;
using ReactiveUI;

namespace CafeDirect.Views
{
    public sealed class MainWindowView : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindowView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}