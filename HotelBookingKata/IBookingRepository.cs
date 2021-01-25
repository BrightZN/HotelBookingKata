using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingKata
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetBookingsAsync(HotelId hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut);
        Task SaveBookingAsync(Booking booking);
    }
}