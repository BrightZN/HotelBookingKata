using System.Threading.Tasks;

namespace HotelBookingKata
{
    public interface IHotelRepository
    {
        Task SaveHotelAsync(Hotel hotel);

        /// <summary>
        /// Throws a <see cref="HotelNotFoundException"/> when a Hotel is not found for the supplied Id.
        /// </summary>
        /// <param name="hotelId"></param>
        /// <exception cref="HotelNotFoundException">Thrown</exception>
        /// <returns></returns>
        Task<Hotel> GetHotelByIdAsync(HotelId hotelId);
    }
}