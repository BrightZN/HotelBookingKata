using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingKata
{
    public class BookingSchedule
    {
        public BookingSchedule(HotelId hotelId, IEnumerable<Booking> bookings)
        {
            HotelId = hotelId;

            _bookings = bookings;
        }

        public HotelId HotelId { get; set; }

        private readonly IEnumerable<Booking> _bookings;

        public bool CannotAccomodateBookingFor(RoomType roomType, DateTime checkIn, DateTime checkOut)
        {
            foreach(var booking in _bookings.Where(b => b.RoomType == roomType))
            {
                if (booking.OverlapsWith(checkIn, checkOut))
                    return true;

            }

            return false;
        }
    }
}