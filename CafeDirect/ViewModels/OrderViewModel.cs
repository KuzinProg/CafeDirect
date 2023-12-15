using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reactive;
using System.Runtime.Serialization;
using Avalonia.Data.Converters;
using CafeDirect.Context;
using CafeDirect.Models;
using ReactiveUI;

namespace CafeDirect.ViewModels;

public class OrderViewModel : ReactiveObject, IRoutableViewModel
{
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    private RoutingState router = new RoutingState();
    private string _clientcount;
    
    public ReactiveCommand<Unit, IRoutableViewModel> CancelCommand { get; }
    public ReactiveCommand<Unit, Unit> OrderCommand { get; }
    
    public RoutingState Router
    {
        get => router;
        set => this.RaiseAndSetIfChanged(ref router, value);
    }
    
    public class Status
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
    
    private List<Status> statuses = new List<Status>
    {
        new Status {Name = "Администратор", Code = "admin"},
        new Status {Name = "Повар", Code = "cook"},
        new Status {Name = "Администратор", Code = "admin"},
        new Status {Name = "Повар", Code = "cook"},
        new Status {Name = "Администратор", Code = "admin"},
    };
    
    public string ClientsCount
    {
        get => _clientcount;
        set => this.RaiseAndSetIfChanged(ref _clientcount, value);
    }
    
    public OrderViewModel(IScreen screen)
    {
        /*HostScreen = screen;
        CancelCommand = ReactiveCommand.CreateFromObservable(() =>
            HostScreen.Router.NavigateAndReset.Execute(new (HostScreen)));
        OrderCommand = ReactiveCommand.Create(Reg);*/
        
    }
}