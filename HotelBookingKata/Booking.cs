using System;

namespace HotelBookingKata
{
    public class Booking
    {
        public Booking(BookingId bookingId, HotelId hotelId, EmployeeId employeeId, RoomType roomType, DateTime checkIn, DateTime checkOut)
        {
            BookingId = bookingId;
            HotelId = hotelId;
            EmployeeId = employeeId;
            RoomType = roomType;
            CheckIn = checkIn;
            CheckOut = checkOut;
        }

        public BookingId BookingId { get; }
        public HotelId HotelId { get; }
        public EmployeeId EmployeeId { get; }
        public RoomType RoomType { get; }
        public DateTime CheckIn { get; }
        public DateTime CheckOut { get; }

        public bool CoincidesWith(DateTime checkIn, DateTime checkOut)
        {
            return Matches(checkIn, checkOut)
                || Wraps(checkIn)
                || Wraps(checkOut)
                || CheckIn > checkIn && checkOut > CheckOut
                || CheckIn == checkIn && checkOut > CheckOut
                || CheckIn > checkIn && checkOut == CheckOut;
        }

        private bool Wraps(DateTime time)
        {
            return CheckIn < time && time < CheckOut;
        }

        private bool Matches(DateTime checkIn, DateTime checkOut)
        {
            return CheckIn == checkIn && CheckOut == checkOut;
        }
    }
}