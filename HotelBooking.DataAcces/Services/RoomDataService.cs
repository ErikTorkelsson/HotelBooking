using HotelBooking.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DataAcces.Services
{
    public class RoomDataService : IRoomDataService
    {
        private readonly HotelBookingDbContext _dbContext;

        public RoomDataService(HotelBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Room>> GetRooms()
        {
            using (var context = _dbContext)
            {
                return await context.Rooms.ToListAsync();
            }
        }
    }
}
