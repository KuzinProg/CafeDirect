using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using Avalonia.Data.Converters;
using CafeDirect.Context;
using CafeDirect.Models;
using DynamicData;
using Microsoft.EntityFrameworkCore;
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
        public ReactiveCommand<Unit, IObservable<IRoutableViewModel>> ExitCommand { get; }
        private DataBaseContext context;

        public RoutingState Router
        {
            get => router;
            set => this.RaiseAndSetIfChanged(ref router, value);
        }

        private ObservableCollection<Employee> _employees;

        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set => this.RaiseAndSetIfChanged(ref _employees, value);
        }
        public ObservableCollection<Order> Orders { get; }

        private Employee _currentEmployee;
        public Employee CurrentEmployee
        {
            get => _currentEmployee;
            set => this.RaiseAndSetIfChanged(ref _currentEmployee, value);
        }

        private DateTime _currentDate;

        public DateTime CurrentDate
        {
            get => _currentDate;
            set => this.RaiseAndSetIfChanged(ref _currentDate, value);
        }

        private IRoutableViewModel prev;

        public AdminControlViewModel(IScreen screen)
        {
            HostScreen = screen;
            prev = screen.Router.NavigationStack.Last();

            context = new DataBaseContext();
            Employees = new ObservableCollection<Employee>(context.Employees);
            Orders = new ObservableCollection<Order>(context.Orders);
            EditEmployeeCommand = ReactiveCommand.Create(EditEmployee);
            RegistrationCommand = ReactiveCommand.CreateFromObservable(() =>
                HostScreen.Router.NavigateAndReset.Execute(new RegistrationControlViewModel(HostScreen)));
            ExitCommand = ReactiveCommand.Create(() => HostScreen.Router.NavigateAndReset.Execute(prev));
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
            context = new DataBaseContext();
            context.Employees.Load();
            Employees.Clear();
            Employees.AddRange(context.Employees);
        }
    }
}