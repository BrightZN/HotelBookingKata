using System;
using System.Threading.Tasks;

namespace HotelBookingKata.Tests
{
    internal class InMemoryBookingScheduleProvider : IBookingScheduleProvider
    {
        public BookingSchedule BookingSchedule { get; set; }

        public Task<BookingSchedule> GetBookingScheduleAsync(HotelId hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
        {
            if (BookingSchedule != null && BookingSchedule.HotelId == hotelId)
                return Task.FromResult(BookingSchedule);

            throw new HotelNotFoundException();
        }
    }
}