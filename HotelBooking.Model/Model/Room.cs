using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Model.Model
{
    public class Room
    {
        public int RoomId { get; set; }
        public int Beds { get; set; }
        public int SQMeters { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public string Name => $"Antal rum: {Beds}. Kvadratmeter: {SQMeters} Pris: {Price} Rating: {Rating}";
        public ICollection<Booking> Bookings { get; set; }
    }
}
