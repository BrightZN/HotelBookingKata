using System.Threading.Tasks;

namespace HotelBookingKata
{
    public interface IHotelRepository
    {
        Task SaveHotelAsync(Hotel hotel);
        Task<Hotel> GetHotelByIdAsync(HotelId hotelId);
    }
}