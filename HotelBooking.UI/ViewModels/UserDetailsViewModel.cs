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
using System.Windows;
using System.Windows.Data;

namespace HotelBooking.UI.ViewModels
{
    public class UserDetailsViewModel : BindableBase
    {
        public ObservableCollection<Booking> Bookings { get; set; }
        public DelegateCommand EditBookingCommand { get; set; }
        public DelegateCommand LogOutCommand { get; set; }

        private Booking _selectedBooking;

        public Booking SelectedBooking
        {
            get { return _selectedBooking; }
            set { SetProperty(ref _selectedBooking, value); }
        }


        private User _user;

        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }


        private string _fullName;

        public string FullName
        {
            get { return _fullName; }
            set { SetProperty(ref _fullName, value); }
        }

        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        public UserDetailsViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            Bookings = new ObservableCollection<Booking>();
            eventAggregator.GetEvent<LoginEvent>().Subscribe(UserReceived);
            EditBookingCommand = new DelegateCommand(Execute, CanExecute);
            LogOutCommand = new DelegateCommand(LogOutExecute, LogOutCanExecute);
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
        }

        private bool LogOutCanExecute()
        {
            return true;
        }

        private void LogOutExecute()
        {
            User user = null;
            _eventAggregator.GetEvent<LoginEvent>().Publish(user);
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute()
        {
            if(SelectedBooking !=null)
            {
                _eventAggregator.GetEvent<EditBookingEvent>().Publish(SelectedBooking);
                _regionManager.RequestNavigate("ContentRegion", "EditBookingView");
            }
            else
            {
                MessageBox.Show("Du måste välja bokning");
            }
            
        }

        private void UserReceived(User user)
        {
            if(user != null)
            {
                Bookings.Clear();
                User = user;
                foreach (var booking in user.Bookings)
                {
                    Bookings.Add(booking);
                }
                FullName = $"{user.FirstName} {user.LastName}";
                Debug.WriteLine(user.FirstName);
            }
            else if(user == null)
            {
                var p = new NavigationParameters();
                p.Add("message", $"Du har nu loggat ut");
                _regionManager.RequestNavigate("ContentRegion", "MessageView", p);
            }
            
        }
    }
}
