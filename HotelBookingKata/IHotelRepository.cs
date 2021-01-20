using System.Threading.Tasks;

namespace HotelBookingKata
{
    public interface IHotelRepository
    {
        Task AddHotelAsync(Hotel hotel);
    }
}