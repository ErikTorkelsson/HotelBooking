using HotelBooking.Model.Model;
using System.Threading.Tasks;

namespace HotelBooking.DataAcces.Services
{
    public interface IBookingDataService
    {
        Task DeleteBooking(Booking booking);
        Task EditBooking(Booking booking);
        Task SaveBooking(Booking booking);
    }
}