using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingKata.Tests
{
    public class HotelServiceTests
    {
        [Fact]
        public async Task AddHotelAsync_ValidHotelIdAndName_CreatesNewHotel()
        {
            var hotelRepository = new InMemoryHotelRepository();
            var hotelService = new HotelService(hotelRepository);

            var hotelId = new HotelId("h4ck");
            var hotelName = new HotelName("Hacker's Paradise");

            await hotelService.AddHotelAsync(hotelId, hotelName);

            Hotel savedHotel = hotelRepository.SavedHotel;

            Assert.NotNull(savedHotel);
            Assert.Equal(hotelId, savedHotel.Id);
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
