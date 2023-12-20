using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using CafeDirect.Models;
using ReactiveUI;
using CafeDirect.Context;
using DynamicData;
using Microsoft.EntityFrameworkCore;

namespace CafeDirect.ViewModels
{

    [DataContract]
    public class CookControlViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }
        private DataBaseContext context;
        private RoutingState router = new RoutingState();
        public ReactiveCommand<Unit, Unit> PreparingCommand { get; }
        public ReactiveCommand<Unit, Unit> ReadyCommand { get; }
        public ObservableCollection<Order> _orders;
        public ReactiveCommand<Unit, IRoutableViewModel> ExitCommand { get; }

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => this.RaiseAndSetIfChanged(ref _orders, value);
        }

        private Order _currentOrder;

        public Order CurrentOrder
        {
            get => _currentOrder;
            set => this.RaiseAndSetIfChanged(ref _currentOrder, value);
        }

        public RoutingState Router
        {
            get => router;
            set => this.RaiseAndSetIfChanged(ref router, value);
        }

        public CookControlViewModel(IScreen screen)
        {
            HostScreen = screen;
            PreparingCommand = ReactiveCommand.Create(Preparing);
            ReadyCommand = ReactiveCommand.Create(Ready);
            context = new DataBaseContext();
            context.OrderItems.Load();
            context.Menus.Load();
            _orders = new ObservableCollection<Order>(context.Orders.Where(o=>o.Status == "new" || o.Status == "preparing"));
            ExitCommand = ReactiveCommand.CreateFromObservable(() =>
                HostScreen.Router.NavigateAndReset.Execute(new AuthControlViewModel(HostScreen)));
        }

        void Preparing()
        {
            CurrentOrder.Status = "preparing";
            context.SaveChanges();
            Orders.Clear();
            context = new DataBaseContext();
            context.OrderItems.Load();
            context.Menus.Load();
            Orders.AddRange(context.Orders.Where(o=>o.Status == "new" || o.Status == "preparing"));
        }

        void Ready()
        {
            CurrentOrder.Status = "ready";
            context.SaveChanges();
            Orders.Clear();
            context = new DataBaseContext();
            context.OrderItems.Load();
            context.Menus.Load();
            Orders.AddRange(context.Orders.Where(o=>o.Status == "new" || o.Status == "preparing"));
        }
    }
}