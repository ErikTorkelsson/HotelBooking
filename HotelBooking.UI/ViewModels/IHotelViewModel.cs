using HotelBooking.Model.Model;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace HotelBooking.UI.ViewModels
{
    public interface IHotelViewModel
    {
        DelegateCommand GetHotelsCommand { get; set; }
        ObservableCollection<Hotel> Hotels { get; set; }
        Hotel SelectedHotel { get; set; }
        DelegateCommand ShowHotelCommand { get; set; }
        string Text { get; set; }

        Task LoadHotels();
    }
}