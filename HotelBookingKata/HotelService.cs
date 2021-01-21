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

        public async Task SetRoomAsync(HotelId hotelId, RoomNumber roomNumber, RoomType roomType)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);

            hotel.SetRoom(roomNumber, roomType);

            await _hotelRepository.SaveHotelAsync(hotel);
        }

        public async Task<HotelInfo> FindHotelByIdAsync(HotelId hotelId)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);

            return new HotelInfo(hotel.Rooms);
        }
    }
}