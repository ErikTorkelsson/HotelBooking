using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.Model.Model;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.DataAcces
{
    public static class DataSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                    new Hotel { HotelId = 1, Name = "Scandic Hotel", Address = "Torskarigatan 5"},
                    new Hotel { HotelId = 2, Name = "Sankt Olof Hotel", Address = "Moravägen 11G" },
                    new Hotel { HotelId = 3, Name = "Skinnargården", Address = "Grönlandsvägen 24" }

            );

            modelBuilder.Entity<Room>().HasData(
                    new Room { RoomId = 1, Beds = 1,Price = 300,Rating = 4.3, SQMeters = 15, HotelId = 1 },
                    new Room { RoomId = 2, Beds = 2,Price = 400,Rating = 3.7, SQMeters = 15, HotelId = 1 },
                    new Room { RoomId = 3, Beds = 3,Price = 500,Rating = 4.2, SQMeters = 15, HotelId = 1 },
                    new Room { RoomId = 4, Beds = 1,Price = 300,Rating = 4.2, SQMeters = 15, HotelId = 2 },
                    new Room { RoomId = 5, Beds = 2,Price = 400,Rating = 4.5, SQMeters = 15, HotelId = 2 },
                    new Room { RoomId = 6, Beds = 3,Price = 500,Rating = 4.3, SQMeters = 15, HotelId = 2 },
                    new Room { RoomId = 7, Beds = 1,Price = 300,Rating = 3.5, SQMeters = 15, HotelId = 3 },
                    new Room { RoomId = 8, Beds = 2,Price = 400,Rating = 2.8, SQMeters = 15, HotelId = 3 },
                    new Room { RoomId = 9, Beds = 3,Price = 500,Rating = 2.5, SQMeters = 15, HotelId = 3 }

            );
        }
    }
}
