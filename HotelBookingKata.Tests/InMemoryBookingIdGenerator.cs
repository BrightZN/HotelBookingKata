using System.Threading.Tasks;

namespace HotelBookingKata.Tests
{
    internal class InMemoryBookingIdGenerator : IBookingIdGenerator
    {
        public Task<BookingId> GenerateBookingIdAsync()
        {
            return Task.FromResult(new BookingId("xyz"));
        }
    }
}