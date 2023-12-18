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

namespace CafeDirect.ViewModels
{
    [DataContract]
    public class AdminControlViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }
        private RoutingState router = new RoutingState();

        public ReactiveCommand<Unit, IRoutableViewModel> RegistrationCommand { get; }
        public ReactiveCommand<Unit, Unit> EditEmployeeCommand { get; }
        public ReactiveCommand<Unit, Unit> FireEmployeeCommand { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> ExitCommand { get; }
        private DataBaseContext context;

        public RoutingState Router
        {
            get => router;
            set => this.RaiseAndSetIfChanged(ref router, value);
        }

        public ObservableCollection<Employee> Employees { get; }
        public ObservableCollection<Order> Orders { get; }

        private Employee _currentEmployee;
        public Employee CurrentEmployee
        {
            get => _currentEmployee;
            set => this.RaiseAndSetIfChanged(ref _currentEmployee, value);
        }

        public AdminControlViewModel(IScreen screen)
        {
            HostScreen = screen;
            context = new DataBaseContext();
            Employees = new ObservableCollection<Employee>(context.Employees);
            Orders = new ObservableCollection<Order>(context.Orders);
            EditEmployeeCommand = ReactiveCommand.Create(EditEmployee);
            RegistrationCommand = ReactiveCommand.CreateFromObservable(() =>
                HostScreen.Router.NavigateAndReset.Execute(new RegistrationControlViewModel(HostScreen)));
            ExitCommand = ReactiveCommand.CreateFromObservable(() =>
                HostScreen.Router.NavigateAndReset.Execute(new AuthControlViewModel(HostScreen)));
            FireEmployeeCommand = ReactiveCommand.Create(FireEmployee);
        }

        public void EditEmployee()
        {
            HostScreen.Router.NavigateAndReset.Execute(new RegistrationControlViewModel(HostScreen, CurrentEmployee));
        }

        public void FireEmployee()
        {
            CurrentEmployee.Status = "fired";
            context.SaveChanges();
            // TODO: Обновление DataGrid
            HostScreen.Router.NavigateAndReset.Execute(new AdminControlViewModel(HostScreen));
        }
    }
}