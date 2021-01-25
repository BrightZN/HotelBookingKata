using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingKata.Tests
{
    public class BookingServiceTests
    {
        private readonly InMemoryHotelRepository _hotelRepository;
        private readonly InMemoryCompanyRepository _companyRepository;
        private readonly InMemoryEmployeeRepository _employeeRepository;
        private readonly BookingPolicyService _bookingPolicyService;

        private readonly BookingService _sut;

        public BookingServiceTests()
        {
            _hotelRepository = new InMemoryHotelRepository();
            _companyRepository = new InMemoryCompanyRepository();
            _employeeRepository = new InMemoryEmployeeRepository();

            _bookingPolicyService = new BookingPolicyService(_companyRepository, _employeeRepository);

            _sut = new BookingService(_hotelRepository, _bookingPolicyService);
        }

        [Fact]
        public async Task BookAsync_CheckInSameAsCheckOut_ThrowsException()
        {
            var employeeId = new EmployeeId("RE-1234");
            var hotelId = new HotelId("JBS");

            DateTime checkIn = new DateTime(2021, 1, 25);
            DateTime checkOut = checkIn;

            await Assert.ThrowsAsync<BookingCheckInDateException>(() => _sut.BookAsync(employeeId, hotelId, RoomType.Standard, checkIn, checkOut));
        }

        [Fact]
        public async Task BookAsync_CheckInWithinOneDayOfCheckOut_ThrowsException()
        {
            var employeeId = new EmployeeId("RE-1234");
            var hotelId = new HotelId("JBS");

            DateTime checkIn = new DateTime(2021, 1, 25);
            DateTime checkOut = checkIn.AddHours(8);

            await Assert.ThrowsAsync<BookingCheckInDateException>(() => _sut.BookAsync(employeeId, hotelId, RoomType.Standard, checkIn, checkOut));
        }

        [Fact]
        public async Task BookAsync_HotelDoesNotOfferRoomType_ThrowsException()
        {
            var employeeId = new EmployeeId("RE-1234");
            var hotelId = new HotelId("JBS");
            var hotelName = new HotelName("JBS Suites");

            DateTime checkIn = new DateTime(2021, 1, 25);
            DateTime checkOut = checkIn.AddDays(3);

            var hotel = new Hotel(hotelId, hotelName, new RoomTypeConfig(RoomType.Standard, 5));

            _hotelRepository.SavedHotel = hotel;

            await Assert.ThrowsAsync<BookingHotelDoesNotOfferRoomTypeException>(() => _sut.BookAsync(employeeId, hotelId, RoomType.Presidential, checkIn, checkOut));
        }

        [Fact]
        public async Task BookAsync_BookingPoliciesDoNotCoverRoomType_ThrowsException()
        {
            var employeeId = new EmployeeId("RE-1234");
            var hotelId = new HotelId("JBS");
            var hotelName = new HotelName("JBS Suites");

            DateTime checkIn = new DateTime(2021, 1, 25);
            DateTime checkOut = checkIn.AddDays(3);

            var hotel = new Hotel(hotelId, hotelName, new RoomTypeConfig(RoomType.Standard, 5));

            _hotelRepository.SavedHotel = hotel;

            var companyId = new CompanyId("123");

            var company = new Company(companyId, new BookingPolicy(RoomType.Standard));

            await Assert.ThrowsAsync<BookingPolicyException>(() => _sut.BookAsync(employeeId, hotelId, RoomType.Standard, checkIn, checkOut));
        }
    }
}
