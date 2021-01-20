using System.Collections.Generic;

namespace HotelBookingKata
{
    public class Room
    {
        public Room(RoomNumber number, RoomType type)
        {
            Number = number;
            Type = type;
        }

        public RoomNumber Number { get; set; }
        public RoomType Type { get; set; }
    }
}