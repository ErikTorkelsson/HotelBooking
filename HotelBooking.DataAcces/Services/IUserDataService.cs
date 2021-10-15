using HotelBooking.Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.DataAcces.Services
{
    public interface IUserDataService
    {
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetUsers();
        Task SaveUser(User user);
    }
}