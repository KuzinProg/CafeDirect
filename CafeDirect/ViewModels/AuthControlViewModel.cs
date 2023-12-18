using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using CafeDirect.Context;
using CafeDirect.Models;
using ReactiveUI;

namespace CafeDirect.ViewModels
{
    [DataContract]
    public class AuthControlViewModel : ReactiveObject, IRoutableViewModel, IScreen
    {
        private string _password;
        private string _login;

        private RoutingState router = new RoutingState();

        public AuthControlViewModel(IScreen screen)
        {
            HostScreen = screen;
            EnterCommand = ReactiveCommand.Create(Enter);
        }

        public ReactiveCommand<Unit, Unit> EnterCommand { get; }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string Login
        {
            get => _login;
            set => this.RaiseAndSetIfChanged(ref _login, value);
        }

        void Enter()
        {
            DataBaseContext context = new DataBaseContext();
            Employee employee = context.Employees.FirstOrDefault(e => e.Password == Password && e.Login == Login);
#if DEBUG
            employee = context.Employees.FirstOrDefault(e => e.Password == "123456" && e.Login == "admin");
            if (employee != null)
                HostScreen.Router.NavigateAndReset.Execute(new AdminControlViewModel(HostScreen));
#else
            if (employee != null)
            {
                if (employee.Role == "admin")
                    HostScreen.Router.NavigateAndReset.Execute(new AdminControlViewModel(HostScreen));
                else if (employee.Role == "waiter")
                    HostScreen.Router.NavigateAndReset.Execute(new WaiterControlViewModel(HostScreen));
                else if (employee.Role == "cook")
                    HostScreen.Router.NavigateAndReset.Execute(new CookControlViewModel(HostScreen));
                else
                {
                    // TODO: Вывести признак ошибки в пароле или логине
                }
            }
#endif
        }

        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        public RoutingState Router
        {
            get => router;
            set => this.RaiseAndSetIfChanged(ref router, value);
        }
    }
}