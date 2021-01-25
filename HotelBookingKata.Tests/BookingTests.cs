using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HotelBookingKata.Tests
{
    public class BookingTests
    {
        private readonly DateTime _checkIn;
        private readonly DateTime _checkOut;

        private readonly Booking _sut;

        public BookingTests()
        {
            _checkIn = new DateTime(2021, 1, 25);
            _checkOut = _checkIn.AddDays(3);


            _sut = new Booking(
                new BookingId("BK-0014"),
                new HotelId("HT-001"),
                new EmployeeId("EMP-00001"),
                RoomType.Standard,
                _checkIn,
                _checkOut);
        }

        [Fact]
        public void OverlapsWith_CheckInAndCheckOutAreTheSame_ReturnsTrue()
        {
            Assert.True(_sut.OverlapsWith(_checkIn, _checkOut));
        }

        [Fact]
        public void OverlapsWith_CheckOutBeforeBookingCheckIn_ReturnsFalse()
        {
            Assert.False(_sut.OverlapsWith(_checkIn.AddDays(4), _checkOut.AddDays(4)));
        }
    }
}
