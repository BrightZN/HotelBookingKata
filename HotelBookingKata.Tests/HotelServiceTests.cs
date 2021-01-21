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
        public async Task AddHotelAsync_ExistingHotel_ThrowsException()
        {
            var hotelId = new HotelId("h4ck");
            var hotelName = new HotelName("Hacker's Paradise");

            var existingHotel = new Hotel(hotelId, hotelName);

            _hotelRepository.SavedHotel = existingHotel;

            var newHotelName = new HotelName("1337 Street");

            await Assert.ThrowsAsync<HotelAlreadyExistsException>(() => _sut.AddHotelAsync(hotelId, newHotelName));
        }

        [Fact]
        public async Task SetRoomAsync_ExistingHotelAndNewRoomInfo_AddsNewRoom()
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
        public async Task SetRoomAsync_ExistingHotelAndUpdatedRoomInfo_UpdatesExistingRoom()
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

        [Fact]
        public async Task SetRoomAsync_NoHotel_ThrowsException()
        {
            var hotelId = new HotelId("h4ck");
            var roomNumber = new RoomNumber("1");
            var roomType = RoomType.Standard;

            await Assert.ThrowsAsync<HotelNotFoundException>(() => _sut.SetRoomAsync(hotelId, roomNumber, roomType));
        }

        [Fact]
        public async Task FindHotelByIdAsync_ExistingHotelWith2StandardRooms_ReturnsHotelInfo()
        {
            var hotelId = new HotelId("h4ck");
            var hotelName = new HotelName("Hacker's Paradise");

            var room1 = new Room(new RoomNumber("1"), RoomType.Standard);
            var room2 = new Room(new RoomNumber("2"), RoomType.Standard);

            var hotel = new Hotel(hotelId, hotelName, room1, room2);

            _hotelRepository.SavedHotel = hotel;

            var hotelInfo = await _sut.FindHotelByIdAsync(hotelId);

            Assert.NotNull(hotelInfo);

            Assert.Equal(2, hotelInfo.GetCountFor(RoomType.Standard));
            Assert.Equal(0, hotelInfo.GetCountFor(RoomType.Presidential));
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
