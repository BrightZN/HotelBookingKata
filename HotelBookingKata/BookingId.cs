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
            this.Value = value;
        }

        public string Value { get; }
    }
}