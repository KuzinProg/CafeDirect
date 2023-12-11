using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using CafeDirect.Context;
using ReactiveUI;

namespace CafeDirect.ViewModels
{
    public class RegistrationControlViewModel : ReactiveObject, IRoutableViewModel, IScreen
    {
        private string _password;
        private string _login;
        private string _firstname;
        private string _lastname;
        private string _middlename;
        private Role _role;
        public IScreen HostScreen { get; }

        private RoutingState router = new RoutingState();
        
        public ReactiveCommand<Unit, IRoutableViewModel> CancelCommand { get; }
        public ReactiveCommand<Unit, Unit> RegCommand { get; }
        
        public RegistrationControlViewModel(IScreen screen)
        {
            HostScreen = screen;
            // TODO: Замена View
            CancelCommand = ReactiveCommand.CreateFromObservable(() =>
                HostScreen.Router.NavigateAndReset.Execute(new AuthControlViewModel(HostScreen)));
            LoadPhotoCommand = ReactiveCommand.Create(LoadPhoto);
        }
        
        public class Role
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        private List<Role> roles = new List<Role>
        {
            new Role {Name = "Администратор", Code = "admin"},
            new Role {Name = "Повар", Code = "cook"},
            new Role {Name = "Официант", Code = "waiter"},
        };

        public List<Role> Roles {
            get => roles;
            set => this.RaiseAndSetIfChanged(ref roles, value);
        }

        public RoutingState Router
        {
            get => router;
            set => this.RaiseAndSetIfChanged(ref router, value);
        }
        
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
        
        public string FirstName
        {
            get => _firstname;
            set => this.RaiseAndSetIfChanged(ref _firstname, value);
        }

        public Role RoleValue
        {
            get => _role;
            set => this.RaiseAndSetIfChanged(ref _role, value);
        }

        public string LastName
        {
            get => _lastname;
            set => this.RaiseAndSetIfChanged(ref _lastname, value);
        }
        
        public string MiddleName
        {
            get => _middlename;
            set => this.RaiseAndSetIfChanged(ref _middlename, value);
        }

        private void Reg()
        {
            
        }

        public ReactiveCommand<Unit, Unit> LoadPhotoCommand { get; }
        public string? UrlPathSegment { get; }

        private async void LoadPhoto()
        {
            /*var topLevel = TopLevel.GetTopLevel();
            var file = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions());*/
        }
    }
}