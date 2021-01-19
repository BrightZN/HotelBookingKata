using System;
using System.Threading.Tasks;

namespace HotelBookingKata.Actors
{
    public class Manager
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;

        public Manager(IHotelRepository hotelRepository, IRoomRepository roomRepository)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
        }

        public async Task AddHotelAsync(HotelId hotelId, HotelName hotelName)
        {
            var hotel = new Hotel(hotelId, hotelName);

            await _hotelRepository.SaveHotelAsync(hotel);
        }

        public async Task SetRoomTypeAsync(HotelId hotelId, RoomNumber roomNumber, RoomType roomType)
        {
            var room = new Room(roomNumber, roomType, hotelId);

            await _roomRepository.SaveRoomAsync(room);
        }

        public async Task<Hotel> GetHotelById(HotelId hotelId)
        {
            return await _hotelRepository.GetHotelByIdAsync(hotelId);
        }
    }
}