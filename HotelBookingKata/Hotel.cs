using System.Collections.Generic;

namespace HotelBookingKata
{
    public class Hotel
    {
        public Hotel(HotelId id)
        {
            Id = id;
        }

        public HotelId Id { get; set; }
    }
}