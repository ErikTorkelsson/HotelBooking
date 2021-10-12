using HotelBooking.DataAcces.Services;
using HotelBooking.Model.Model;
using HotelBooking.UI.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HotelBooking.UI.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IUserDataService _service;
        private readonly IEventAggregator _ea;
        private readonly IRegionManager _regionManager;

        public ObservableCollection<User> Users { get; set; }

        public DelegateCommand<object> LoginCommand { get; set; }
        public DelegateCommand<string> NavigateCommand { get; set; }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                //OnPropertyChanged(nameof(_email));
                //SetProperty(ref _email, value);
            }
        }

        private string _passWord;

        public string PassWord
        {
            get { return _passWord; }
            set
            {
                _passWord = value;
                //OnPropertyChanged(nameof(PassWord));
            }
        }

        public LoginViewModel(IUserDataService service, IEventAggregator ea, IRegionManager regionManager)
        {
            _service = service;
            _ea = ea;
            _regionManager = regionManager;
            Users = new ObservableCollection<User>();
            LoginCommand = new DelegateCommand<object>(Execute);
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private async void Execute(object parameter)
        {
            int id;
            await LoadUsers();
            
            // Detta är nog inte best paractice men funkar för skoluppgift.
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password.ToString();

            id = LoginCheck(password);
            

            if (id > 0)
            {
                User user = GetUserById(id);
                _ea.GetEvent<LoginEvent>().Publish(user);
                var p = new NavigationParameters();
                p.Add("message", $"Välkommen {user.FirstName}");
                _regionManager.RequestNavigate("ContentRegion", "MessageView", p);
            }
            else
            {
                MessageBox.Show("Fel lösenord eller email");
            }
        }

        private void Navigate(string viewName)
        {
            _regionManager.RequestNavigate("ContentRegion", viewName);
        }

        //private bool CanExecute()
        //{
        //    return true;
        //}

        //private async void Execute(object parameter)
        //{
        //    int id = 0;
        //    await LoadUsers();
        //    User user = GetUserById(id);

        //    var passwordBox = parameter as PasswordBox;
        //    var password = passwordBox.Password.ToString();

        //    id = LoginCheck(password);

        //    _ea.GetEvent<LoginEvent>().Publish(user);

        //    if (id > 0)
        //    {
        //        MessageBox.Show("Hittad");
        //        var p = new NavigationParameters();
        //        p.Add("message", $"Välkommen {user.FirstName}");
        //        _regionManager.RequestNavigate("ContentRegion", "MessageView", p);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Inte hittad");
        //    }
        //}

        public async Task LoadUsers()
        {
            var users = await _service.GetUsers();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        public int LoginCheck(string password)
        {
            int id = 0;

            foreach (var user in Users)
            {
                if (user.Email == Email && user.PassWord == password)
                {
                    id = user.UserId;
                }
            }

            return id;
        }

        public User GetUserById(int id)
        {
            User user = Users.FirstOrDefault(u => u.UserId == id);

            return user;
        }

    }
}
