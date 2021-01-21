using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingKata
{
    public class HotelInfo
    {
        private readonly IEnumerable<Room> _rooms;

        public HotelInfo(IEnumerable<Room> rooms) => _rooms = rooms;

        public int GetCountFor(RoomType roomType) => _rooms.Count(r => r.Type == roomType);
    }
}