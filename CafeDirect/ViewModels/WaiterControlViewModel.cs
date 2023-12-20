using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using ReactiveUI;
using CafeDirect.Context;
using CafeDirect.Models;
using Microsoft.EntityFrameworkCore;

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
        public ReactiveCommand<Unit, IRoutableViewModel> ExitCommand { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> AddNewOrderCommand { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> EditOrderCommand { get; }
        public ReactiveCommand<Unit, Task> AllOrdersReportCommand { get; }

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

        public WaiterControlViewModel(IScreen screen, Employee employee)
        {
            CurrentWaiter = employee;
            HostScreen = screen;
            DataBaseContext context = new DataBaseContext();
            context.Employees.Load();
            context.Menus.Load();
            Orders = new ObservableCollection<Order>(context.Orders.Where(o=>o.Waiter == CurrentWaiter.EmployeeId));
            AddNewOrderCommand = ReactiveCommand.CreateFromObservable(() =>
                HostScreen.Router.NavigateAndReset.Execute(new OrderViewModel(HostScreen, CurrentWaiter)));
            EditOrderCommand = ReactiveCommand.CreateFromObservable(() =>
                HostScreen.Router.NavigateAndReset.Execute(new OrderViewModel(HostScreen, CurrentOrder)));
            ExitCommand = ReactiveCommand.CreateFromObservable(() =>
                HostScreen.Router.NavigateAndReset.Execute(new AuthControlViewModel(HostScreen)));
            AllOrdersReportCommand = ReactiveCommand.Create(AllOrdersReport);
        }

        private async Task AllOrdersReport()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"{Orders.First().Date}");
            foreach (Order order in Orders)
            {
                result.AppendLine($"{order.OrderId}\t{order.Place}\t{order.ClientsCount}\t{order.OrderItems.Sum(o=>o.MenuItemNavigation.Price)}");
            }

            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
                desktop.MainWindow?.StorageProvider is not { } provider)
                throw new NullReferenceException("Missing StorageProvider instance.");

            var file = await provider.SaveFilePickerAsync(new FilePickerSaveOptions()
            {
                Title = "Open Text File",

            });
            if (file != null)
                await File.AppendAllTextAsync(Encoding.UTF8.GetString(Encoding.Default.GetBytes(file.Path.LocalPath)), result.ToString());
        }
    }
}