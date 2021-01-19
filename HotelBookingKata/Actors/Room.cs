using System.Collections.Generic;

namespace HotelBookingKata
{
    public class Room
    {
        public Room(RoomNumber roomNumber, RoomType roomType, HotelId hotelId)
        {
            Number = roomNumber;
            Type = roomType;
            HotelId = hotelId;
        }

        public RoomNumber Number { get; }
        public RoomType Type { get; }
        public HotelId HotelId { get; set; }
    }
}