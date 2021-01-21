using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingKata.Tests
{
    public class HotelServiceTests
    {
        private readonly InMemoryHotelRepository _hotelRepository;

        private readonly HotelService _sut;

        public HotelServiceTests()
        {
            _hotelRepository = new InMemoryHotelRepository();

            _sut = new HotelService(_hotelRepository);
        }

        [Fact]
        public async Task AddHotelAsync_ValidHotelIdAndName_CreatesNewHotel()
        {
            var hotelId = new HotelId("h4ck");
            var hotelName = new HotelName("Hacker's Paradise");

            await _sut.AddHotelAsync(hotelId, hotelName);

            var savedHotel = _hotelRepository.SavedHotel;

            Assert.NotNull(savedHotel);
            Assert.Equal(hotelId, savedHotel.Id);
            Assert.Equal(hotelName, savedHotel.Name);
            Assert.Equal(0, savedHotel.NumberOfRooms);
        }

        [Fact]
        public async Task SetRoomAsync_ValidHotelIdAndNewRoomInfo_AddsNewRoom()
        {
            var hotelId = new HotelId("h4ck");
            var hotelName = new HotelName("Hacker's Paradise");

            var roomNumber = new RoomNumber("1");
            var roomType = RoomType.Standard;

            var existingHotel = new Hotel(hotelId, hotelName);

            _hotelRepository.SavedHotel = existingHotel;

            await _sut.SetRoomAsync(hotelId, roomNumber, roomType);

            var savedHotel = _hotelRepository.SavedHotel;

            var addedRoom = savedHotel.Rooms.Single();

            Assert.Equal(roomNumber, addedRoom.Number);
            Assert.Equal(roomType, addedRoom.Type);
        }

        [Fact]
        public async Task SetRoomAsync_ValidHotelIdAndUpdatedRoomInfo_UpdatesExistingRoom()
        {
            var hotelId = new HotelId("h4ck");
            var hotelName = new HotelName("Hacker's Paradise");

            var roomNumber = new RoomNumber("1");
            var originalRoomType = RoomType.Standard;

            var existingRoom = new Room(roomNumber, originalRoomType);

            var existingHotel = new Hotel(hotelId, hotelName, existingRoom);

            _hotelRepository.SavedHotel = existingHotel;

            var newRoomType = RoomType.Presidential;

            await _sut.SetRoomAsync(hotelId, roomNumber, newRoomType);

            var savedHotel = _hotelRepository.SavedHotel;

            var updatedRoom = savedHotel.Rooms.Single();

            Assert.Equal(newRoomType, updatedRoom.Type);
        }
    }

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
            if (SavedHotel.Id == hotelId)
                return Task.FromResult(SavedHotel);

            throw new HotelNotFoundException();
        }
    }
}
