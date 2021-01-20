using System;
using System.Collections.Generic;
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
            var roomNumber = new RoomNumber("1");
            var roomType = RoomType.Standard;

            await _sut.SetRoomAsync(hotelId, roomNumber, roomType);

        }
    }

    internal class InMemoryHotelRepository : IHotelRepository
    {
        public Hotel SavedHotel { get; set; }

        public Task AddHotelAsync(Hotel hotel)
        {
            SavedHotel = hotel;

            return Task.CompletedTask;
        }
    }
}
