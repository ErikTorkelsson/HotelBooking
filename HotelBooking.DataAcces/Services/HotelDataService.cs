using HotelBooking.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DataAcces.Services
{
    public class HotelDataService : IHotelDataService
    {
        private readonly HotelBookingDbContext _dbContext;

        public HotelDataService(HotelBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Hotel>> GetHotels()
        {
            return await _dbContext.Hotels.Include(h => h.Rooms).ThenInclude(r => r.Bookings).ToListAsync();
        }
    }
}
