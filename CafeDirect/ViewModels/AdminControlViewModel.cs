using System.Runtime.Serialization;
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

        public AdminControlViewModel()
        {

        }
    }
}