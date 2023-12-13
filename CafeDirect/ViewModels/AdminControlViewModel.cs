using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.Serialization;
using Avalonia.Data.Converters;
using CafeDirect.Context;
using CafeDirect.Models;
using ReactiveUI;

namespace CafeDirect.ViewModels
{
    [DataContract]
    public class AdminControlViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }
        private RoutingState router = new RoutingState();

        public RoutingState Router
        {
            get => router;
            set => this.RaiseAndSetIfChanged(ref router, value);
        }

        public ObservableCollection<Employee> Employees { get; }
        public ObservableCollection<Order> Orders { get; }

        public AdminControlViewModel()
        {
            DataBaseContext context = new DataBaseContext();
            Employees = new ObservableCollection<Employee>(context.Employees);
            Orders = new ObservableCollection<Order>(context.Orders);
        }
    }
}