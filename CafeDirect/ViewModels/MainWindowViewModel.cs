using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;
using CafeDirect.Context;
using CafeDirect.Models;
using ReactiveUI;

namespace CafeDirect.ViewModels
{
    [DataContract]
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        private readonly ReactiveCommand<Unit, Unit> auth;
        //private readonly ReactiveCommand<Unit, Unit> registration;
        private RoutingState router = new RoutingState();

        [DataMember]
        public RoutingState Router
        {
            get => router;
            set => this.RaiseAndSetIfChanged(ref router, value);
        }

        public MainWindowViewModel()
        {
            var canAuth = this.WhenAnyObservable(o => o.Router.CurrentViewModel)
                .Select(current => !(current is AuthControlViewModel));
            auth = ReactiveCommand.Create(() => { Router.Navigate.Execute(new AuthControlViewModel()); }, canAuth);
        }

        public ICommand AuthWindow => auth;
        //public ICommand RegistrationWindow => registration;
    }
}