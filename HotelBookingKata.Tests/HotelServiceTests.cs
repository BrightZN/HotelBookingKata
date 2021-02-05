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
        public async Task SetRoomAsync_ExistingHotelAndNewRoomInfo_AddsNewRoomType()
        {
            var hotelId = new HotelId("h4ck");
            var hotelName = new HotelName("Hacker's Paradise");

            int roomCount = 1;
            var roomType = RoomType.Standard;

            var existingHotel = new Hotel(hotelId, hotelName);

            _hotelRepository.SavedHotel = existingHotel;

            await _sut.SetRoomAsync(hotelId, roomType, roomCount);

            Assert.Equal(1, existingHotel.RoomCountFor(roomType));
            Assert.Equal(0, existingHotel.RoomCountFor(RoomType.Presidential));
        }

        [Fact]
        public async Task SetRoomAsync_ExistingHotelAndUpdatedRoomInfo_UpdatesExistingRoom()
        {
            var hotelId = new HotelId("h4ck");
            var hotelName = new HotelName("Hacker's Paradise");

            int roomCount = 1;
            var roomType = RoomType.Standard;

            var roomTypeConfig = new RoomTypeConfig(roomType, roomCount);

            var existingHotel = new Hotel(hotelId, hotelName, roomTypeConfig);

            _hotelRepository.SavedHotel = existingHotel;

            await _sut.SetRoomAsync(hotelId, roomType, 2);

            Assert.Equal(2, existingHotel.RoomCountFor(RoomType.Standard));
        }

        [Fact]
        public async Task SetRoomAsync_NoHotel_ThrowsException()
        {
            var hotelId = new HotelId("h4ck");
            var roomCount = 1;
            var roomType = RoomType.Standard;

            await Assert.ThrowsAsync<HotelNotFoundException>(() => _sut.SetRoomAsync(hotelId, roomType, roomCount));
        }

        [Fact]
        public async Task FindHotelByIdAsync_ExistingHotel_ReturnsHotelInfo()
        {
            var hotelId = new HotelId("h4ck");
            var hotelName = new HotelName("Hacker's Paradise");

            var roomTypeConfig1 = new RoomTypeConfig(RoomType.Standard, 5);
            var roomTypeConfig2 = new RoomTypeConfig(RoomType.Presidential, 3);

            var hotel = new Hotel(hotelId, hotelName, roomTypeConfig1, roomTypeConfig2);

            _hotelRepository.SavedHotel = hotel;

            // going to introduce a HotelMapper
            var hotelMapper = new TestHotelInfoMapper();
            
            var hotelInfo = await _sut.FindHotelByIdAsync(hotelId, hotelMapper);

            Assert.NotNull(hotelInfo);
            
            Assert.Equal(hotelId, hotel.Id);
            Assert.Equal(hotelName, hotel.Name);
            Assert.Equal(5, hotelInfo.RoomCountFor(RoomType.Standard));
            Assert.Equal(3, hotelInfo.RoomCountFor(RoomType.Presidential));
        }
    }

    public class TestHotelInfoMapper : IHotelMapper<Hotel>
    {
        public Hotel Map(Hotel hotel) => hotel;
    }
}
