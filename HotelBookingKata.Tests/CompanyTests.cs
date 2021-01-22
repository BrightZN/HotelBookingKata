using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HotelBookingKata.Tests
{
    public class CompanyTests
    {
        private readonly CompanyId _companyId = new CompanyId("Test");

        [Fact]
        public void CanBook_NoPolicy_ReturnsTrue()
        {
            var sut = new Company(_companyId);

            Assert.True(sut.CanBook(RoomType.Standard));
        }

        [Fact]
        public void CanBook_RoomTypeNotInPolicy_ReturnsFalse()
        {
            var policy = new BookingPolicy(RoomType.Standard);

            var sut = new Company(_companyId, policy);

            Assert.False(sut.CanBook(RoomType.Presidential));
        }

        [Fact]
        public void CanBook_RoomTypeInPolicy_ReturnsTrue()
        {
            var policy = new BookingPolicy(RoomType.Standard);

            var sut = new Company(_companyId, policy);

            Assert.True(sut.CanBook(RoomType.Standard));
        }

        [Fact]
        public void ChangePolicy_NewPolicy_UpdatedPolicy()
        {
            var existingPolicy = new BookingPolicy(RoomType.Standard);

            var sut = new Company(_companyId, existingPolicy);

            var newPolicy = new BookingPolicy(RoomType.Standard, RoomType.Presidential);

            sut.ChangePolicy(newPolicy);

            Assert.True(sut.CanBook(RoomType.Standard));
            Assert.True(sut.CanBook(RoomType.Presidential));

        }
    }
}
