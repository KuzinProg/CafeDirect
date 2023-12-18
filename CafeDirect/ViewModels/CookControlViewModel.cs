using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using CafeDirect.Models;
using ReactiveUI;
using CafeDirect.Context;

namespace CafeDirect.ViewModels
{

    [DataContract]
    public class CookControlViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }
        private RoutingState router = new RoutingState();
        public ObservableCollection<Order> Orders { get; }

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
            DataBaseContext context = new DataBaseContext();
            Orders = new ObservableCollection<Order>(context.Orders);
        }
    }
}