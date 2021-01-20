using System.Collections.Generic;

namespace HotelBookingKata
{
    public class Hotel
    {
        public Hotel(HotelId id, HotelName name)
        {
            Id = id;
            Name = name;
        }

        public HotelId Id { get; set; }
        public HotelName Name { get; set; }
    }
}