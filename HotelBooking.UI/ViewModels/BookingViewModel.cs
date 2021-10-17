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
        private bool _oneWeek;
        public bool OneWeek
        {
            get { return _oneWeek; }
            set 
            { 
                SetProperty(ref _oneWeek, value);
                if(OneWeek)
                {
                    TwoWeeks = false;
                    Transportation = false;
                    Pool = false;
                    Breakfast = false;
                    AllInclusive = false;
                    TotalPrice = Room.Price * 7;
                }
                
            }
        }

        private bool _twoWeeks;
        public bool TwoWeeks
        {
            get { return _twoWeeks; }
            set
            {
                SetProperty(ref _twoWeeks, value);
                if (TwoWeeks)
                {
                    OneWeek = false;
                    Transportation = false;
                    Pool = false;
                    Breakfast = false;
                    AllInclusive = false;
                    TotalPrice = Room.Price * 14;
                }
            }
        }

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

        private bool _transportation;
        public bool Transportation
        {
            get { return _transportation; }
            set
            { 
                SetProperty(ref _transportation, value);
                SetExtrasPrice(Transportation, 200);
            }
        }

        private bool _pool;
        public bool Pool
        {
            get { return _pool; }
            set
            { 
                SetProperty(ref _pool, value);
                SetExtrasPrice(Pool, 150);
            }
        }

        private bool _breakfast;
        public bool Breakfast
        {
            get { return _breakfast; }
            set
            { 
                SetProperty(ref _breakfast, value);
                SetExtrasPrice(Breakfast, 300);
            }
        }

        private bool _allInclusive;
        public bool AllInclusive
        {
            get { return _allInclusive; }
            set
            { 
                SetProperty(ref _allInclusive, value);
                SetExtrasPrice(AllInclusive, 500);
            }
        }


        private double _totalPrice;
        public double TotalPrice
        {
            get { return _totalPrice; }
            set 
            {
                SetProperty(ref _totalPrice, value);
            }
        }

        public bool IsLoggedIn { get; set; }

        private Room _room;
        public Room Room
        {
            get { return _room; }
            set { SetProperty(ref _room, value); }
        }

        private User _user;
        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public DelegateCommand BookCommand { get; set; }

        private readonly IUserDataService _userService;
        private readonly IBookingDataService _service;
        private readonly IEventAggregator _ea;
        private readonly IRegionManager _regionManager;



        public BookingViewModel(IUserDataService userService ,IBookingDataService service, IEventAggregator ea, IRegionManager regionManager)
        {
            BookCommand = new DelegateCommand(BookExecute, BookCanExecute);
            ea.GetEvent<BookingEvent>().Subscribe(RoomReceived);
            ea.GetEvent<LoginEvent>().Subscribe(UserReceived);
            IsLoggedIn = false;
            _userService = userService;
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

        private async void BookExecute()
        {
            await AddBooking();
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

        public void SetExtrasPrice(bool type, double price)
        {
            if (type)
            {
                if (OneWeek)
                {
                    TotalPrice += price;
                }
                else if (TwoWeeks)
                {
                    TotalPrice += price * 2;
                }
            }
            else
            {
                if (OneWeek)
                {
                    TotalPrice -= price;
                }
                else if (TwoWeeks)
                {
                    TotalPrice -= price * 2;
                }
            }
        }

        public async Task AddBooking()
        {
            if (IsLoggedIn)
            {
                if(OneWeek || TwoWeeks)
                {
                    Booking booking = new Booking
                    {
                        UserId = User.UserId,
                        RoomId = Room.RoomId,
                        Weeks = OneWeek ? 1 : 2,
                        StartDate = StartDate,
                        EndDate = OneWeek ? StartDate.AddDays(7) : StartDate.AddDays(14),
                        Transportation = Transportation,
                        Pool = Pool,
                        Breakfast = Breakfast,
                        AllInclusive = AllInclusive,
                        TotalPrice = TotalPrice
                    };

                    await _service.SaveBooking(booking);
                    var user = await _userService.GetUserByEmail(User.Email);
                    User = user;
                    _ea.GetEvent<UpdateHotelsEvent>().Publish();
                    _ea.GetEvent<LoginEvent>().Publish(User);

                    var p = new NavigationParameters();
                    p.Add("message", $"Tack för din bokning!");
                    _regionManager.RequestNavigate("ContentRegion", "MessageView", p);
                }
                else
                {
                    MessageBox.Show("Du måste välje en vecka eller två veckor");
                }
                
            }
            else
            {
                MessageBox.Show("Du måste vara inloggad för att kunna boka");
            }
        }
    }
}
