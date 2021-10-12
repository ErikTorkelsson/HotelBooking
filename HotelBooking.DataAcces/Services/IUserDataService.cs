using HotelBooking.Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DataAcces.Services
{
    public interface IUserDataService
    {
        Task<List<User>> GetUsers();
        Task SaveUser(User user);
    }
}