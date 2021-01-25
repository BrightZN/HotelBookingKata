using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingKata
{
    public class BookingSchedule
    {
        public BookingSchedule(Hotel hotel, IEnumerable<Booking> bookings)
        {
            Hotel = hotel;

            _bookings = bookings;
        }

        public Hotel Hotel { get; }

        private readonly IEnumerable<Booking> _bookings;

        public bool CannotAccomodateBookingFor(RoomType roomType, DateTime checkIn, DateTime checkOut)
        {
            int roomCount = Hotel.RoomCountFor(roomType);

            int occupiedRooms = _bookings.Count(b => b.RoomType == roomType && b.CoincidesWith(checkIn, checkOut));

            return roomCount <= occupiedRooms;
        }
    }
}