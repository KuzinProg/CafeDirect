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
        private RoutingState router = new RoutingState();

        [DataMember]
        public RoutingState Router
        {
            get => router;
            set => this.RaiseAndSetIfChanged(ref router, value);
        }

        public MainWindowViewModel()
        {
            Router.Navigate.Execute(new AuthControlViewModel(this));
        }
    }
}