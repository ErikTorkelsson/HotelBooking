using HotelBooking.Model.Model;
using HotelBooking.UI.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace HotelBooking.UI.Views
{
    /// <summary>
    /// Interaction logic for BookingView
    /// </summary>
    public partial class BookingView : UserControl
    {
        public List<Booking> Bookings { get; set; }

        public BookingView(IEventAggregator eventAggregator)
        {
            InitializeComponent();

            Bookings = new List<Booking>();
            eventAggregator.GetEvent<BookingEvent>().Subscribe(RoomReceived);
        }

        private void RoomReceived(Room room)
        {
            Bookings.Clear();
            StartDatePicker.BlackoutDates.Clear();
            foreach (var booking in room.Bookings)
            {
                Bookings.Add(booking);
            }
            SetBlackoutDates();
        }

        public void SetBlackoutDates()
        {
            try
            {
                foreach (var booking in Bookings)
                {
                    StartDatePicker.BlackoutDates.Add(new CalendarDateRange(
                    booking.StartDate,
                    booking.EndDate
                ));
                }
            }
            catch(ArgumentException e)
            {
                Debug.WriteLine(e);
            }
            
        }

        
    }
}
