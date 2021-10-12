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
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelBooking.UI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;


        private User _user;

        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private string _email = "Logga in";

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public bool isLoggedIn { get; set; }


        public DelegateCommand<string> NavigateCommand { get; set; }
        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            NavigateCommand = new DelegateCommand<string>(Navigate);
            _regionManager = regionManager;
            ea.GetEvent<LoginEvent>().Subscribe(UserReceived);
        }


        private void UserReceived(User user)
        {
            User = user;
            Email = user.Email;
            foreach (var booking in User.Bookings)
            {
                Debug.WriteLine(booking.RoomId);
            }
            isLoggedIn = true;
        }

        private void Navigate(string viewName)
        {
            if(viewName == "LoginView")
            {
                if(isLoggedIn)
                {
                    var p = new NavigationParameters();
                    p.Add("message", $"Du är redan inloggad");
                    _regionManager.RequestNavigate("ContentRegion", "MessageView", p);
                }
                else
                {
                    _regionManager.RequestNavigate("ContentRegion", viewName);
                }
            }
            else if(viewName == "HotelView")
            {
                _regionManager.RequestNavigate("ContentRegion", viewName);
            }
            else if(viewName == "UserDetailsView")
            {
                if(isLoggedIn)
                {
                    _regionManager.RequestNavigate("ContentRegion", viewName);
                }
                else
                {
                    MessageBox.Show("Du är inte inloggad.");
                }
            }
            
        }

    }
}
