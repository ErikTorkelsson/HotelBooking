using HotelBooking.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DataAcces.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly HotelBookingDbContext _dbContext;

        public UserDataService(HotelBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetUsers()
        {
            using (var context = _dbContext)
            {
                return await context.Users.Include(u => u.Bookings).ToListAsync();
            }
        }

        public async Task SaveUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

    }
}
