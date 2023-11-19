using System;
using CafeDirect.Context;
using CafeDirect.Models;
using ReactiveUI;

namespace CafeDirect.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _contentViewModel;
    public AuthControlViewModel AuthWindow { get; }

    public MainWindowViewModel()
    {
        AuthWindow = new AuthControlViewModel();
        _contentViewModel = AuthWindow;
    }

    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public void Registration()
    {
        _contentViewModel = new RegistrationControlViewModel();
    }
}