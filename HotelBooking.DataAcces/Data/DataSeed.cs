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

            modelBuilder.Entity<User>().HasData(
                    new User { UserId = 1, FirstName = "Erik", LastName = "Torkelsson",Address = "enriktiggata 5",PhoneNumber = 0723457689, Email = "erik.torkelsson@hotmail.com", PassWord = "asdf123" },
                    new User { UserId = 2, FirstName = "Per", LastName = "Andersson",Address = "påriktigtgatan 8",PhoneNumber = 0707358635, Email = "per.andersson@gmail.com", PassWord = "asdf123" },
                    new User { UserId = 3, FirstName = "Lena", LastName = "Karlsson",Address = "asdfgatan 7",PhoneNumber = 0733578723, Email = "lena.karlsson@outlook.com", PassWord = "asdf123" }

            );
        }
    }
}
