using HotelBooking.DataAcces.Services;
using HotelBooking.Model.Model;
using HotelBooking.UI.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.UI.ViewModels
{
    public class EditBookingViewModel : BindableBase
    {
        private Booking _booking;

        public Booking Booking
        {
            get { return _booking; }
            set { SetProperty(ref _booking, value); }
        }

        private User _user;

        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private int _roomId;

        public int RoomId
        {
            get { return _roomId; }
            set { SetProperty(ref _roomId, value); }
        }


        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value); }
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
        private readonly IEventAggregator _eventAggregator;
        private readonly IBookingDataService _service;
        private readonly IRegionManager _regionManager;

        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        public bool AllInclusive
        {
            get { return _allInclusive; }
            set { SetProperty(ref _allInclusive, value); }
        }

        public EditBookingViewModel(IEventAggregator eventAggregator, IBookingDataService service, IRegionManager regionManager)
        {
            eventAggregator.GetEvent<EditBookingEvent>().Subscribe(BookingReceived);
            eventAggregator.GetEvent<LoginEvent>().Subscribe(UserReceived);
            _eventAggregator = eventAggregator;
            _service = service;
            _regionManager = regionManager;
            EditCommand = new DelegateCommand(EditExecute, EditCanExecute);
            DeleteCommand = new DelegateCommand(DeleteExecute, DeleteCanExecute);
        }

        

        private bool DeleteCanExecute()
        {
            return true;
        }

        private void DeleteExecute()
        {
            _service.DeleteBooking(Booking);
            User.Bookings.Remove(Booking);
            _eventAggregator.GetEvent<LoginEvent>().Publish(User);

            var p = new NavigationParameters();
            p.Add("message", $"Du har tagit bort din bokning");
            _regionManager.RequestNavigate("ContentRegion", "MessageView", p);
        }

        private bool EditCanExecute()
        {
            return true;
        }

        private void EditExecute()
        {
            User.Bookings.Remove(Booking);
            var booking = new Booking
            {
                UserId = User.UserId,
                RoomId = RoomId,
                StartDate = StartDate,
                EndDate = EndDate,
                Transportation = Transportation,
                Pool = Pool,
                Breakfast = Breakfast,
                AllInclusive = AllInclusive
            };
            User.Bookings.Add(booking);
            _service.EditBooking(booking);
            _eventAggregator.GetEvent<LoginEvent>().Publish(User);

            var p = new NavigationParameters();
            p.Add("message", $"Din Bokining är uppdaterad");
            _regionManager.RequestNavigate("ContentRegion", "MessageView", p);
        }

        private void UserReceived(User user)
        {
            User = user;
        }

        private void BookingReceived(Booking booking)
        {
            Booking = booking;
            RoomId = booking.RoomId;
            StartDate = booking.StartDate;
            EndDate = booking.EndDate;
            Transportation = booking.Transportation;
            Pool = booking.Pool;
            Breakfast = booking.Breakfast;
            AllInclusive = booking.AllInclusive;

        }
    }
}
