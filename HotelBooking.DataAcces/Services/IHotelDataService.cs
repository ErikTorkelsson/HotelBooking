using HotelBooking.Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DataAcces.Services
{
    public interface IHotelDataService
    {
        Task<List<Hotel>> GetHotels();
    }
}