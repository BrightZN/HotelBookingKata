using System.Threading.Tasks;

namespace HotelBookingKata.Tests
{
    internal class InMemoryHotelRepository : IHotelRepository
    {
        public Hotel SavedHotel { get; set; }

        public Task SaveHotelAsync(Hotel hotel)
        {
            SavedHotel = hotel;

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<Hotel> GetHotelByIdAsync(HotelId hotelId)
        {
            if (SavedHotel != null && SavedHotel.Id == hotelId)
                return Task.FromResult(SavedHotel);

            throw new HotelNotFoundException();
        }

        public Task<bool> HasHotelWithIdAsync(HotelId hotelId)
        {
            return Task.FromResult(SavedHotel != null && SavedHotel.Id == hotelId);
        }
    }
}
