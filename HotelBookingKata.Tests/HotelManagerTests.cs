using HotelBookingKata.Actors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingKata.Tests
{
    public class HotelManagerTests
    {
        private readonly InMemoryHotelRepository _hotelRepository;
        private readonly InMemoryRoomRepository _roomRepository;

        private readonly Manager _manager;

        public HotelManagerTests()
        {
            _hotelRepository = new InMemoryHotelRepository();
            _roomRepository = new InMemoryRoomRepository();

            _manager = new Manager(_hotelRepository, _roomRepository);
        }

        [Fact]
        public async Task Manager_Can_Add_Hotel()
        {
            var hotelId = new HotelId("H4CK");
            var hotelName = new HotelName("Hacker's Paradise");

            await _manager.AddHotelAsync(hotelId, hotelName);

            var savedHotel = _hotelRepository.SavedHotel;

            Assert.NotNull(savedHotel);
            Assert.Equal(hotelId, savedHotel.Id);
            Assert.Equal(hotelName, savedHotel.Name);
        }

        [Fact]
        public async Task Manager_Can_Set_Room_Type()
        {
            var hotelId = new HotelId("H4CK");
            var roomNumber = new RoomNumber(1);
            var roomType = RoomType.Standard;

            await _manager.SetRoomTypeAsync(hotelId, roomNumber, roomType);


            var savedRoom = _roomRepository.SavedRoom;

            Assert.NotNull(savedRoom);
            Assert.Equal(roomNumber, savedRoom.Number);
            Assert.Equal(roomType, savedRoom.Type);
            Assert.Equal(hotelId, savedRoom.HotelId);
        }

        [Fact]
        public async Task Manager_Can_Get_Hotel_By_Id()
        {
            var hotelId = new HotelId("H4CK");
            var hotelName = new HotelName("Hacker's Paradise");

            var hotel = new Hotel(hotelId, hotelName);

            _hotelRepository.SavedHotel = hotel;

            var retrievedHotel = await _manager.GetHotelById(hotelId);

            Assert.Equal(hotel, retrievedHotel);
        }
    }

    internal class InMemoryRoomRepository : IRoomRepository
    {
        public InMemoryRoomRepository()
        {
        }

        public Room SavedRoom { get; set; }

        public Task SaveRoomAsync(Room room)
        {
            SavedRoom = room;

            return Task.CompletedTask;
        }
    }

    internal class InMemoryHotelRepository : IHotelRepository
    {
        public Hotel SavedHotel { get; internal set; }

        public Task<Hotel> GetHotelByIdAsync(HotelId hotelId)
        {
            if (hotelId == SavedHotel.Id)
                return Task.FromResult(SavedHotel);

            throw new HotelNotFoundException();
        }

        public Task SaveHotelAsync(Hotel hotel)
        {
            SavedHotel = hotel;

            return Task.CompletedTask;
        }
    }
}
