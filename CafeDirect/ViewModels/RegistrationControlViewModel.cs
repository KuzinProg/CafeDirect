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

        public RegistrationControlViewModel()
        {

        }
    }
}