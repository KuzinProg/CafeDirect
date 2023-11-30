using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using CafeDirect.Context;
using ReactiveUI;

namespace CafeDirect.ViewModels
{

    [DataContract]
    public class AuthControlViewModel : ReactiveObject, IRoutableViewModel
    {
        private string _password;
        private string _login;

        public AuthControlViewModel()
        {
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
            var employee = context.Employees.FirstOrDefault(e => e.Password == Password && e.Login == Login);
            if (employee != null)
            {

            }
        }

        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }
    }
}