using System.Linq;
using System.Reactive;
using CafeDirect.Context;
using ReactiveUI;

namespace CafeDirect.ViewModels
{

    public class RegistrationControlViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment { get; }

        public IScreen HostScreen { get; }

        private RoutingState router = new RoutingState();

        public RoutingState Router
        {
            get => router;
            set => this.RaiseAndSetIfChanged(ref router, value);
        }

        public RegistrationControlViewModel()
        {

        }
    }
}