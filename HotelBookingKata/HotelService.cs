using System;
using System.Threading.Tasks;

namespace HotelBookingKata
{
    public class HotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task AddHotelAsync(HotelId hotelId, HotelName hotelName)
        {
            var hotel = new Hotel();//id: hotelId, name: hotelName

            await _hotelRepository.AddHotelAsync(hotel);
        }
    }
}