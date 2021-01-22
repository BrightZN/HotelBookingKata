using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HotelBookingKata.Tests
{
    public class EmployeeTests
    {
        private readonly EmployeeId _employeeId = new EmployeeId("RE-0987");
        private readonly CompanyId _companyId = new CompanyId("Test");

        [Fact]
        public void CanBook_NoPolicy_ReturnsTrue()
        {
            var sut = new Employee(_employeeId, _companyId);

            Assert.True(sut.CanBook(RoomType.Standard));
        }

        [Fact]
        public void CanBook_RoomTypeNotInPolicy_ReturnsFalse()
        {
            var employeePolicy = new BookingPolicy(RoomType.Presidential);

            var sut = new Employee(_employeeId, _companyId, employeePolicy);

            Assert.False(sut.CanBook(RoomType.Standard));
        }
    }
}
