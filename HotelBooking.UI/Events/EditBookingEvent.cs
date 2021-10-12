using HotelBooking.Model.Model;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.UI.Events
{
    class EditBookingEvent : PubSubEvent<Booking>
    {
    }
}
