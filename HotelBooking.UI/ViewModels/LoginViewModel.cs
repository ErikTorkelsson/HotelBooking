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
using System.Diagnostics;
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
            set { SetProperty(ref _email, value); }
        }

        private string _passWord;
        public string PassWord
        {
            get { return _passWord; }
            set { SetProperty(ref _passWord, value); }
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
            try
            {
                User user = await _service.GetUserByEmail(Email);
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password.ToString();

                LoginCheck(user, password);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }      
        }

        private void Navigate(string viewName)
        {
            _regionManager.RequestNavigate("ContentRegion", viewName);
        }

        public async Task LoadUsers()
        {
            var users = await _service.GetUsers();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        public void LoginCheck(User user, string password)
        {
            bool verified = user != null ? BCrypt.Net.BCrypt.Verify(password, user.PassWord) : false;

            if (verified)
            {
                _ea.GetEvent<LoginEvent>().Publish(user);
                var p = new NavigationParameters();
                p.Add("message", $"Välkommen {user.FirstName}");
                _regionManager.RequestNavigate("ContentRegion", "MessageView", p);
                Email = "";
                PassWord = "";
            }
            else
            {
                MessageBox.Show("Fel lösenord eller email");
            }

        }

    }
}
