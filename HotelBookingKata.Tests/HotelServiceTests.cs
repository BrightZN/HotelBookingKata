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
            var hotelService = new HotelService();

            var hotelId = new HotelId("h4ck");
            var hotelName = new HotelName("Hacker's Paradise");

            await hotelService.AddHotelAsync(hotelId, hotelName);


        }
    }
}
