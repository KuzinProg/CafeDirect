using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
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
        public string? UrlPathSegment { get; }

        public IScreen HostScreen { get; }

        private RoutingState router = new RoutingState();

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

        public ReactiveCommand<Unit, Unit> LoadPhotoCommand { get; }

        private async void LoadPhoto()
        {
            var topLevel = TopLevel.GetTopLevel();
            var file = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions());
        }

        public RegistrationControlViewModel()
        {
            LoadPhotoCommand = ReactiveCommand.Create(LoadPhoto);
        }
    }
}