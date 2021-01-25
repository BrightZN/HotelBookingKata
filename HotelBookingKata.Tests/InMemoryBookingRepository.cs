using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingKata.Tests
{
    internal class InMemoryBookingRepository : IBookingRepository
    {
        public List<Booking> Bookings { get; internal set; }

        public Booking SavedBooking { get; internal set; }

        public Task<IEnumerable<Booking>> GetBookingsAsync(HotelId hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
        {
            return Task.FromResult(Bookings as IEnumerable<Booking>);
        }

        public Task SaveBookingAsync(Booking booking)
        {
            SavedBooking = booking;

            return Task.CompletedTask;
        }
    }
}