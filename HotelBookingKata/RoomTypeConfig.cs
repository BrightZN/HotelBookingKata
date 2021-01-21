using System;
using System.Collections.Generic;

namespace HotelBookingKata
{
    public class RoomTypeConfig
    {
        public RoomTypeConfig(RoomType type, int count)
        {
            Type = type;
            Count = count;
        }

        public RoomType Type { get; }
        public int Count { get; set; }

        public bool HasType(RoomType roomType) => Type == roomType;
    }
}