using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using CafeDirect.ViewModels;

namespace CafeDirect.Views;

public partial class RegistrationControlView : ReactiveWindow<RegistrationControlViewModel>
{
    public RegistrationControlView()
    {
        InitializeComponent();
    }
}