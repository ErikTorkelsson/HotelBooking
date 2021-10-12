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
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelBooking.UI.ViewModels
{
    public class LoginViewModel : BindableBase, ILoginViewModel
    {
        private readonly IUserDataService _service;
        private readonly IEventAggregator _ea;
        private readonly IRegionManager _regionManager;

        public ObservableCollection<User> Users { get; set; }

        public DelegateCommand LoginCommand { get; set; }
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
            LoginCommand = new DelegateCommand(Execute, CanExecute);
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string viewName)
        {
            _regionManager.RequestNavigate("ContentRegion", viewName);
        }

        private bool CanExecute()
        {
            return true;
        }

        private async void Execute()
        {
            int id = 0;
            await LoadUsers();
            id = LoginCheck();
            User user = GetUserById(id);

            _ea.GetEvent<LoginEvent>().Publish(user);

            if (id > 0)
            {
                //MessageBox.Show("Hittad");
                var p = new NavigationParameters();
                p.Add("message", $"Välkommen {user.FirstName}");
                _regionManager.RequestNavigate("ContentRegion", "MessageView",p);
            }
            else
            {
                MessageBox.Show("Inte hittad");
            }
        }

        public async Task LoadUsers()
        {
            var users = await _service.GetUsers();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        public int LoginCheck()
        {
            int id = 0;

            foreach (var user in Users)
            {
                if (user.Email == Email && user.PassWord == PassWord)
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
