using System;
using System.Threading.Tasks;

namespace HotelBookingKata
{
    public interface IBookingScheduleProvider
    {
        Task<BookingSchedule> GetBookingScheduleAsync(HotelId hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut);
    }
}