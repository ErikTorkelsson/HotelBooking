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
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HotelBooking.UI.ViewModels
{
    public class BookingViewModel : BindableBase
    {
        
        public DelegateCommand BookCommand { get; set; }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value);}
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }

        private User _user;

        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private bool _transportation;

        public bool Transportation
        {
            get { return _transportation; }
            set { SetProperty(ref _transportation, value); }
        }

        private bool _pool;

        public bool Pool
        {
            get { return _pool; }
            set { SetProperty(ref _pool, value); }
        }

        private bool _breakfast;

        public bool Breakfast
        {
            get { return _breakfast; }
            set { SetProperty(ref _breakfast, value); }
        }

        private bool _allInclusive;

        public bool AllInclusive
        {
            get { return _allInclusive; }
            set { SetProperty(ref _allInclusive, value); }
        }

        public bool IsLoggedIn { get; set; }


        private Room _room;
        private readonly IBookingDataService _service;
        private readonly IEventAggregator _ea;
        private readonly IRegionManager _regionManager;

        public Room Room
        {
            get { return _room; }
            set { SetProperty(ref _room, value); }
        }


        public BookingViewModel(IBookingDataService service, IEventAggregator ea, IRegionManager regionManager)
        {
            BookCommand = new DelegateCommand(BookExecute, BookCanExecute);
            ea.GetEvent<BookingEvent>().Subscribe(RoomReceived);
            ea.GetEvent<LoginEvent>().Subscribe(UserReceived);
            IsLoggedIn = false;
            _service = service;
            _ea = ea;
            _regionManager = regionManager;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        private bool BookCanExecute()
        {
            return true;
        }

        private void BookExecute()
        {
            if(IsLoggedIn)
            {
                Booking booking = new Booking
                {
                    UserId = User.UserId,
                    RoomId = Room.RoomId,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    Transportation = Transportation,
                    Pool = Pool,
                    Breakfast = Breakfast,
                    AllInclusive = AllInclusive
                };

                _service.SaveBooking(booking);
                User.Bookings.Add(booking);
                _ea.GetEvent<UpdateHotelsEvent>().Publish();
                _ea.GetEvent<LoginEvent>().Publish(User);

                var p = new NavigationParameters();
                p.Add("message", $"Tack för din bokning!");
                _regionManager.RequestNavigate("ContentRegion", "MessageView", p);
            }
            else
            {
                MessageBox.Show("Du måste vara inloggad för att kunna boka");
            }
        }

        private void RoomReceived(Room room)
        {
            Room = room;
            Debug.WriteLine(Room.Name);
        }

        private void UserReceived(User user)
        {
            if(user != null)
            {                
                IsLoggedIn = true;
            }
            else if(user == null)
            {
                IsLoggedIn = false;
            }
            User = user;

        }

    }
}
