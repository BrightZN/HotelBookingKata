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
            if (await _hotelRepository.HasHotelWithIdAsync(hotelId))
                throw new HotelAlreadyExistsException();

            var hotel = new Hotel(hotelId, hotelName);

            await _hotelRepository.SaveHotelAsync(hotel);
        }

        public async Task SetRoomAsync(HotelId hotelId, RoomType roomType, int roomCount)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);

            hotel.SetRoom(roomType, roomCount);

            await _hotelRepository.SaveHotelAsync(hotel);
        }

        public async Task<TResult> FindHotelByIdAsync<TResult>(HotelId hotelId, IHotelMapper<TResult> hotelMapper)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);

            return hotelMapper.Map(hotel);
        }
    }
}