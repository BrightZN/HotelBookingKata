using System;

namespace HotelBookingKata
{
    public class BookingId
    {
        public BookingId(Guid id)
            : this(id.ToString())
        {
        }

        public BookingId(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}