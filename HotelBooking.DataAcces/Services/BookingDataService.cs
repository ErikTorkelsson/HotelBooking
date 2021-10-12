using HotelBooking.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DataAcces.Services
{
    public class BookingDataService : IBookingDataService
    {
        private readonly HotelBookingDbContext _dbContext;

        public BookingDataService(HotelBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveBooking(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditBooking(Booking booking)
        {
            try
            {
                _dbContext.Entry(booking).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e) { Debug.WriteLine(e.Message); }
        }

        public async Task DeleteBooking(Booking booking)
        {
            var bookingToRemove = await _dbContext.Bookings.FirstAsync(b => b.BookingId == booking.BookingId);
            _dbContext.Bookings.Remove(bookingToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }
}
