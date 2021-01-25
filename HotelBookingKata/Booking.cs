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

        public bool OverlapsWith(DateTime checkIn, DateTime checkOut)
        {
            return CheckIn == checkIn && CheckOut == checkOut;
        }
    }
}