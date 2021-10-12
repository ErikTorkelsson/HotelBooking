using HotelBooking.Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DataAcces.Services
{
    public interface IRoomDataService
    {
        Task<List<Room>> GetRooms();
    }
}