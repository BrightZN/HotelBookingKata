using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingKata
{
    public class BookingPolicy
    {
        public BookingPolicy(params RoomType[] roomTypes)
        {
            _roomTypes = roomTypes;
        }

        public BookingPolicy(IEnumerable<RoomType> roomTypes)
        {
            _roomTypes = roomTypes;
        }

        public readonly IEnumerable<RoomType> _roomTypes;

        public bool AllowsBookingFor(RoomType roomType)
        {
            return _roomTypes.Contains(roomType);
        }
    }
}