using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using CafeDirect.ViewModels;
using ReactiveUI;

namespace CafeDirect.Views;

public class AdminControlView : ReactiveUserControl<AdminControlViewModel>
{
    public AdminControlView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}