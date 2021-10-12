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

namespace HotelBooking.UI.ViewModels
{
    public class RoomViewModel : BindableBase
    {
        private Hotel _hotel;
        public Hotel Hotel
        {
            get { return _hotel; }
            set { SetProperty(ref _hotel, value); }
        }

        private Room _selectedRoom;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        public Room SelectedRoom
        {
            get { return _selectedRoom; }
            set { SetProperty(ref _selectedRoom, value); }
        }

        public ObservableCollection<Room> Rooms { get; set; }

        public DelegateCommand ShowRoomCommand { get; set; }
        public DelegateCommand<string> SortCommand { get; set; }

        public RoomViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            Rooms = new ObservableCollection<Room>();
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            eventAggregator.GetEvent<HotelDetailEvent>().Subscribe(HotelReceived);
            ShowRoomCommand = new DelegateCommand(Execute, CanExecute);
            SortCommand = new DelegateCommand<string>(Sort);

        }

        private void Sort(string sortAfter)
        {
            if(sortAfter == "price")
            {
                var rooms = new ObservableCollection<Room>(Rooms.OrderByDescending(r => r.Price));
                Rooms.Clear();
                foreach (var room in rooms)
                {
                    Rooms.Add(room);
                }
            }
            else
            {
                var rooms = new ObservableCollection<Room>(Rooms.OrderByDescending(r => r.Rating));
                Rooms.Clear();
                foreach (var room in rooms)
                {
                    Rooms.Add(room);
                }
            }
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute()
        {
            _eventAggregator.GetEvent<BookingEvent>().Publish(SelectedRoom);
            _regionManager.RequestNavigate("ContentRegion", "BookingView");
        }

        private void HotelReceived(Hotel hotel)
        {
            Hotel = hotel;
            Debug.WriteLine(hotel.Name);
            Rooms.Clear();
            foreach (var room in hotel.Rooms)
            {
                Rooms.Add(room);
            }
        }
    }
}
