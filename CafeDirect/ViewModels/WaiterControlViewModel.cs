using System.Collections.ObjectModel;
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

        public RoutingState Router
        {
            get => router;
            set => this.RaiseAndSetIfChanged(ref router, value);
        }

        public WaiterControlViewModel(IScreen screen)
        {
            HostScreen = screen;
            DataBaseContext context = new DataBaseContext();
            Orders = new ObservableCollection<Order>(context.Orders);
        }
    }
}