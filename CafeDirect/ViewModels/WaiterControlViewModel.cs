using System.Collections.ObjectModel;
using System.Reactive;
using System.Runtime.Serialization;
using ReactiveUI;
using CafeDirect.Context;
using CafeDirect.Models;

namespace CafeDirect.ViewModels
{

    [DataContract]
    public class WaiterControlViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }
        private RoutingState router = new RoutingState();
        public ObservableCollection<Order> Orders { get; }
        private Employee? _currentWaiter = null;
        private Order? _currentOrder = null;

        public ReactiveCommand<Unit, IRoutableViewModel> AddNewOrderCommand { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> EditOrderCommand { get; }

        public Employee? CurrentWaiter
        {
            get => _currentWaiter;
            set => this.RaiseAndSetIfChanged(ref _currentWaiter, value);
        }

        public Order? CurrentOrder
        {
            get => _currentOrder;
            set => this.RaiseAndSetIfChanged(ref _currentOrder, value);
        }

        public RoutingState Router
        {
            get => router;
            set => this.RaiseAndSetIfChanged(ref router, value);
        }

        public WaiterControlViewModel(IScreen screen, Employee? employee = null)
        {
            CurrentWaiter = employee;
            HostScreen = screen;
            DataBaseContext context = new DataBaseContext();
            Orders = new ObservableCollection<Order>(context.Orders);
            AddNewOrderCommand = ReactiveCommand.CreateFromObservable(() =>
                HostScreen.Router.NavigateAndReset.Execute(new OrderViewModel(HostScreen, CurrentWaiter)));
            EditOrderCommand = ReactiveCommand.CreateFromObservable(() =>
                HostScreen.Router.NavigateAndReset.Execute(new OrderViewModel(HostScreen, CurrentOrder)));
        }
    }
}