using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reactive;
using System.Runtime.Serialization;
using Avalonia.Data.Converters;
using CafeDirect.Context;
using CafeDirect.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReactiveUI;

namespace CafeDirect.ViewModels;

public class OrderViewModel : ReactiveObject, IRoutableViewModel
{
    public string? UrlPathSegment { get; }
    private DataBaseContext context;
    public IScreen HostScreen { get; }
    private RoutingState router = new RoutingState();
    private int? _clientCount;
    private int? _place;
    private Status _status;
    private DateTime? _date;
    private Employee? _currentWaiter = null;
    private ObservableCollection<Menu> _selectedMenuItems;

    public ObservableCollection<Menu> SelectedMenuItems
    {
        get => _selectedMenuItems;
        set => this.RaiseAndSetIfChanged(ref _selectedMenuItems, value);
    }

    public Employee? CurrentWaiter
    {
        get => _currentWaiter;
        set => this.RaiseAndSetIfChanged(ref _currentWaiter, value);
    }

    public ReactiveCommand<Unit, IRoutableViewModel> CancelCommand { get; }
    public ReactiveCommand<Unit, Unit> OrderCommand { get; }
    public ReactiveCommand<Unit, Unit> AddMenuItemCommand { get; }

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
        new Status {Name = "Принят", Code = "new"},
        new Status {Name = "Оплачен", Code = "paid"},
        new Status {Name = "Готовится", Code = "preparing"},
        new Status {Name = "Готов", Code = "ready"},
        new Status {Name = "Отменён", Code = "canceled"},
    };

    public List<Status> Statuses
    {
        get => statuses;
        set => this.RaiseAndSetIfChanged(ref statuses, value);
    }

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

    public Status StatusValue
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }

    private Menu _currentMenuItem;

    public Menu CurrentMenuItem
    {
        get => _currentMenuItem;
        set => this.RaiseAndSetIfChanged(ref _currentMenuItem, value);
    }

    public ObservableCollection<Menu> MenuItems { get; }

    public OrderViewModel(IScreen screen, Employee? waiter = null)
    {
        context = new DataBaseContext();
        context.Employees.Load();
        context.OrderItems.Load();
        CurrentWaiter = waiter;
        HostScreen = screen;
        CancelCommand = ReactiveCommand.CreateFromObservable(() =>
            HostScreen.Router.NavigateAndReset.Execute(new AdminControlViewModel(HostScreen)));
        OrderCommand = ReactiveCommand.Create(NewOrder);
        context.Menus.Load();
        MenuItems = new ObservableCollection<Menu>(context.Menus);
        SelectedMenuItems = new ObservableCollection<Menu>();
        AddMenuItemCommand = ReactiveCommand.Create(AddMenuItem);
    }

    public OrderViewModel(IScreen screen, Order order) : this(screen, order.WaiterNavigation)
    {
        ClientsCount = order.ClientsCount;
        Place = order.Place;
        StatusValue = statuses.Find(o=>o.Code==order.Status);
        Date = order.Date;
        // TODO: Редактирование заказа. Преобразование в MenuItems
        //OrderItems = new ObservableCollection<OrderItem>(order.OrderItems);
    }

    private void NewOrder()
    {
        // TODO: Проверка корректности
        // TODO: Официанты
        if (CurrentWaiter != null)
        {
            EntityEntry<Order> order = context.Orders.Add(new Order
            {
                Waiter = CurrentWaiter.EmployeeId,
                Status = StatusValue.Code,
                Date = DateTime.Now,
                Place = Place,
                ClientsCount = ClientsCount,
            });
            context.SaveChanges();
            foreach (Menu menu in SelectedMenuItems)
            {
                context.OrderItems.Add(new OrderItem {Order = order.Entity.OrderId, MenuItem = menu.MenuId});
            }
        }

        context.SaveChanges();

        HostScreen.Router.NavigateAndReset.Execute(new WaiterControlViewModel(HostScreen, CurrentWaiter));
    }

    public void AddMenuItem()
    {
        SelectedMenuItems.Add(CurrentMenuItem);
    }
}