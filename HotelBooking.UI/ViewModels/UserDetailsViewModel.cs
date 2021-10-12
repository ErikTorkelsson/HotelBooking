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
using System.Windows.Data;

namespace HotelBooking.UI.ViewModels
{
    public class UserDetailsViewModel : BindableBase
    {
        public ObservableCollection<Booking> Bookings { get; set; }
        public DelegateCommand EditBookingCommand { get; set; }

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
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute()
        {
            _eventAggregator.GetEvent<EditBookingEvent>().Publish(SelectedBooking);
            _regionManager.RequestNavigate("ContentRegion", "EditBookingView");
        }

        private void UserReceived(User user)
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
    }
}
