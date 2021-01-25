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
        public void CoincidesWith_CheckInAndCheckOutAreTheSame_ReturnsTrue()
        {
            Assert.True(_sut.CoincidesWith(_checkIn, _checkOut));
        }

        [Fact]
        public void CoincidesWith_CheckOutBeforeBookingStarts_ReturnsFalse()
        {
            Assert.False(_sut.CoincidesWith(_checkIn.AddDays(4), _checkOut.AddDays(4)));
        }

        [Fact]
        public void CoincidesWith_CheckInDuringBooking_ReturnsTrue()
        {
            Assert.True(_sut.CoincidesWith(_checkIn.AddDays(1), _checkOut));
        }

        [Fact]
        public void CoincidesWith_CheckOutDuringBooking_ReturnsTrue()
        {
            Assert.True(_sut.CoincidesWith(_checkIn, _checkOut.AddDays(-1)));
        }

        [Fact]
        public void CoincidesWith_CheckInAndCheckOutDuringBooking_ReturnsTrue()
        {
            Assert.True(_sut.CoincidesWith(_checkIn.AddDays(1), _checkOut.AddDays(-1)));
        }

        [Fact]
        public void CoincidesWith_CheckInBeforeAndCheckOutAfterBooking_ReturnsTrue()
        {
            Assert.True(_sut.CoincidesWith(_checkIn.AddDays(-1), _checkOut.AddDays(1)));
        }

        [Fact]
        public void CoincidesWith_CheckOutAtStartOfBooking_ReturnsFalse()
        {
            Assert.False(_sut.CoincidesWith(_checkIn.AddDays(-3), _checkIn));
        }

        [Fact]
        public void CoincidesWith_CheckInAtEndOfBooking_ReturnsFalse()
        {
            Assert.False(_sut.CoincidesWith(_checkOut, _checkOut.AddDays(3)));
        }

        [Fact]
        public void CoincidesWith_CheckInWithAndCheckOutAfterBooking_ReturnsTrue()
        {
            Assert.True(_sut.CoincidesWith(_checkIn, _checkOut.AddDays(1)));
        }

        [Fact]
        public void CoincidesWith_CheckInBeforeAndCheckOutWithBooking_ReturnsTrue()
        {
            Assert.True(_sut.CoincidesWith(_checkIn.AddDays(-1), _checkOut));
        }
    }
}
