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
    private int? _clientCount;
    private int? _place;
    private Status? _status;
    private DateTime? _date;
    private Employee? _currentWaiter = null;
    private ObservableCollection<OrderItem> _orderItems;

    public ObservableCollection<OrderItem> OrderItems
    {
        get => _orderItems;
        set => this.RaiseAndSetIfChanged(ref _orderItems, value);
    }

    public Employee? CurrentWaiter
    {
        get => _currentWaiter;
        set => this.RaiseAndSetIfChanged(ref _currentWaiter, value);
    }

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


    private List<Status> statuses = new()
    {
        new Status {Name = "Принят", Code = "active"},
        new Status {Name = "Оплачен", Code = "paid"},
        new Status {Name = "Готовится", Code = "preparing"},
        new Status {Name = "Готов", Code = "ready"},
        new Status {Name = "Отменён", Code = "canceled"},
    };

    public int? ClientsCount
    {
        get => _clientCount;
        set => this.RaiseAndSetIfChanged(ref _clientCount, value);
    }

    public int? Place
    {
        get => _place;
        set => this.RaiseAndSetIfChanged(ref _place, value);
    }

    public DateTime? Date
    {
        get => _date;
        set => this.RaiseAndSetIfChanged(ref _date, ((DateTimeOffset) value).UtcDateTime);
    }

    public Status? StatusValue
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }

    public OrderViewModel(IScreen screen, Employee? waiter = null)
    {
        CurrentWaiter = waiter;
        HostScreen = screen;
        CancelCommand = ReactiveCommand.CreateFromObservable(() =>
            HostScreen.Router.NavigateAndReset.Execute(new AdminControlViewModel(HostScreen)));
        OrderCommand = ReactiveCommand.Create(NewOrder);
    }

    public OrderViewModel(IScreen screen, Order order) : this(screen, order.WaiterNavigation)
    {
        ClientsCount = order.ClientsCount;
        Place = order.Place;
        StatusValue = statuses.Find(o=>o.Code==order.Status);
        Date = order.Date;
        //OrderItems = new ObservableCollection<OrderItem>(order.OrderItems);
    }

    private void NewOrder()
    {
        // TODO: Проверка корректности
        // TODO: Официанты

        DataBaseContext context = new DataBaseContext();
        if (CurrentWaiter != null)
            context.Orders.Add(new Order
            {
                Waiter = CurrentWaiter.EmployeeId,
                Status = StatusValue.Code,
                Date = Date,
                Place = Place,
                ClientsCount = ClientsCount,
            });
        context.SaveChanges();
    }
}