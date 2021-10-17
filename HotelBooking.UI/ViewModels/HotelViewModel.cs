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

namespace HotelBooking.UI.ViewModels
{
    public class HotelViewModel : BindableBase, IHotelViewModel
    {
        private Hotel _selectedHotel;
        public Hotel SelectedHotel
        {
            get { return _selectedHotel; }
            set { SetProperty(ref _selectedHotel, value); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public DelegateCommand GetHotelsCommand { get; set; }
        public DelegateCommand ShowHotelCommand { get; set; }

        public ObservableCollection<Hotel> Hotels { get; set; }
        private readonly IHotelDataService _service;
        private readonly IEventAggregator _ea;
        private readonly IRegionManager _regionManager;


        public HotelViewModel(IHotelDataService service, IEventAggregator ea, IRegionManager regionManager)
        {
            Hotels = new ObservableCollection<Hotel>();
            GetHotelsCommand = new DelegateCommand(GetHotelsExecute, GetHotelsCanExecute);
            ShowHotelCommand = new DelegateCommand(ShowHotelExecute, ShowHotelCanExecute);
            _service = service;
            _ea = ea;
            _ea.GetEvent<UpdateHotelsEvent>().Subscribe(UpdateHotels);
            _regionManager = regionManager;
            _text = "HotelView";
            LoadHotels();

        }

        private async void UpdateHotels()
        {
            await LoadHotels();
        }

        private bool ShowHotelCanExecute()
        {
            return true;
        }

        private void ShowHotelExecute()
        {
            _regionManager.RequestNavigate("ContentRegion", "RoomView");
            _ea.GetEvent<HotelDetailEvent>().Publish(SelectedHotel);
        }

        private bool GetHotelsCanExecute()
        {
            return true;
        }

        private async void GetHotelsExecute()
        {
            await LoadHotels();
        }

        public async Task LoadHotels()
        {
            var hotels = await _service.GetHotels();
            Hotels.Clear();
            foreach (var hotel in hotels)
            {
                Hotels.Add(hotel);
                Debug.WriteLine(hotel.Name);
            }
        }

    }
}
