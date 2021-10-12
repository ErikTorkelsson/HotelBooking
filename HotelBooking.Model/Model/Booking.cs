using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Model.Model
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Transportation { get; set; }
        public bool Pool { get; set; }
        public bool Breakfast { get; set; }
        public bool AllInclusive { get; set; }
        public string Name => $"Bokning från: {StartDate.ToShortDateString()} till: {EndDate.ToShortDateString()}";

    }
}
