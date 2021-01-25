using System.Threading.Tasks;

namespace HotelBookingKata
{
    public interface IBookingIdGenerator
    {
        Task<BookingId> GenerateBookingIdAsync();
    }
}