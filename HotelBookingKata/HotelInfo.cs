using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingKata
{
    public class HotelInfo
    {
        private readonly IEnumerable<RoomTypeConfig> _rooms;

        public HotelInfo(IEnumerable<RoomTypeConfig> rooms) => _rooms = rooms;

        public int GetCountFor(RoomType roomType) => _rooms.Count(r => r.Type == roomType);
    }
}