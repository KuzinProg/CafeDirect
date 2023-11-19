using System.Linq;
using System.Reactive;
using CafeDirect.Context;
using ReactiveUI;

namespace CafeDirect.ViewModels;

public class AuthControlViewModel : ViewModelBase
{
    private string _password;
    private string _login;
    public AuthControlViewModel()
    {
        EnterCommand = ReactiveCommand.Create(Enter);
        RegistrationCommand = ReactiveCommand.Create(Registration);
    }

    public ReactiveCommand<Unit, Unit> EnterCommand { get; }
    public ReactiveCommand<Unit, Unit> RegistrationCommand { get; }

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

    void Registration()
    {

    }

}