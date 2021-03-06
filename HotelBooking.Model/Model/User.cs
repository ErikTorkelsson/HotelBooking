using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Model.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address{ get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
