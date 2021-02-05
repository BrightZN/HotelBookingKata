using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly InMemoryBookingRepository _bookingRepository;

        private readonly BookingService _sut;

        public BookingServiceTests()
        {
            _hotelRepository = new InMemoryHotelRepository();
            _companyRepository = new InMemoryCompanyRepository();
            _employeeRepository = new InMemoryEmployeeRepository();

            _bookingPolicyService = new BookingPolicyService(_companyRepository, _employeeRepository);


            _bookingRepository = new InMemoryBookingRepository();

            _sut = new BookingService(
                _hotelRepository,
                _bookingPolicyService,
                _bookingRepository);
        }

        [Fact]
        public async Task BookAsync_CheckInSameAsCheckOut_ThrowsException()
        {
            var employeeId = new EmployeeId("RE-1234");
            var hotelId = new HotelId("JBS");

            DateTime checkIn = new DateTime(2021, 1, 25);
            DateTime checkOut = checkIn;

            await Assert.ThrowsAsync<BookingCheckOutDateException>(() => _sut.BookAsync(employeeId, hotelId, RoomType.Standard, checkIn, checkOut));
        }

        [Fact]
        public async Task BookAsync_CheckInWithinOneDayOfCheckOut_ThrowsException()
        {
            var employeeId = new EmployeeId("RE-1234");
            var hotelId = new HotelId("JBS");

            DateTime checkIn = new DateTime(2021, 1, 25);
            DateTime checkOut = checkIn.AddHours(8);

            await Assert.ThrowsAsync<BookingCheckOutDateException>(() => _sut.BookAsync(employeeId, hotelId, RoomType.Standard, checkIn, checkOut));
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

            var hotel = new Hotel(hotelId, hotelName, new RoomTypeConfig(RoomType.Standard, 5), new RoomTypeConfig(RoomType.Presidential, 1));

            _hotelRepository.SavedHotel = hotel;

            var companyId = new CompanyId("123");
            var company = new Company(companyId, new BookingPolicy(RoomType.Standard));

            var employee = new Employee(employeeId, companyId);

            _companyRepository.SavedCompany = company;
            _employeeRepository.SavedEmployee = employee;

            await Assert.ThrowsAsync<BookingPolicyException>(() => _sut.BookAsync(employeeId, hotelId, RoomType.Presidential, checkIn, checkOut));
        }

        [Fact]
        public async Task BookAsync_NoRoomsAvailableDuringBookingPeriod_ThrowsException()
        {
            var employeeId = new EmployeeId("RE-1234");
            var hotelId = new HotelId("JBS");
            var hotelName = new HotelName("JBS Suites");

            DateTime checkIn = new DateTime(2021, 1, 25);
            DateTime checkOut = checkIn.AddDays(3);

            var hotel = new Hotel(hotelId, hotelName, new RoomTypeConfig(RoomType.Standard, 5));

            // add 5 bookings to the hotel?
            var bookings = new List<Booking>
            { 
                new Booking(new BookingId(value: "BK-00001"), hotelId, employeeId, RoomType.Standard, checkIn, checkOut),
                new Booking(new BookingId(value: "BK-00002"), hotelId, employeeId, RoomType.Standard, checkIn, checkOut),
                new Booking(new BookingId(value: "BK-00003"), hotelId, employeeId, RoomType.Standard, checkIn, checkOut),
                new Booking(new BookingId(value: "BK-00004"), hotelId, employeeId, RoomType.Standard, checkIn, checkOut),
                new Booking(new BookingId(value: "BK-00005"), hotelId, employeeId, RoomType.Standard, checkIn, checkOut)
            };

            _bookingRepository.Bookings = bookings;

            _hotelRepository.SavedHotel = hotel;

            var companyId = new CompanyId("123");
            var company = new Company(companyId, new BookingPolicy(RoomType.Standard));

            var employee = new Employee(employeeId, companyId);

            _companyRepository.SavedCompany = company;
            _employeeRepository.SavedEmployee = employee;

            await Assert.ThrowsAsync<BookingRoomTypeNotAvailable>(() => _sut.BookAsync(employeeId, hotelId, RoomType.Standard, checkIn, checkOut));
        }

        [Fact]
        public async Task BookAsync_RoomsAvailableForBooking_CreatesAndReturnsBooking()
        {
            var employeeId = new EmployeeId("RE-1234");
            var hotelId = new HotelId("JBS");
            var hotelName = new HotelName("JBS Suites");

            DateTime checkIn = new DateTime(2021, 1, 25);
            DateTime checkOut = checkIn.AddDays(3);

            var hotel = new Hotel(hotelId, hotelName, new RoomTypeConfig(RoomType.Standard, 5));

            _bookingRepository.Bookings = new List<Booking>();
            _hotelRepository.SavedHotel = hotel;

            var companyId = new CompanyId("123");
            var company = new Company(companyId, new BookingPolicy(RoomType.Standard));

            var employee = new Employee(employeeId, companyId);

            _companyRepository.SavedCompany = company;
            _employeeRepository.SavedEmployee = employee;

            var booking = await _sut.BookAsync(employeeId, hotelId, RoomType.Standard, checkIn, checkOut);

            Assert.NotNull(booking);
            Assert.NotNull(_bookingRepository.SavedBooking);
            
            Assert.NotNull(booking.BookingId);
            Assert.Equal(checkIn, booking.CheckIn);
            Assert.Equal(checkOut, booking.CheckOut);
            Assert.Equal(employeeId, booking.EmployeeId);
            Assert.Equal(hotelId, booking.HotelId);
            Assert.Equal(RoomType.Standard, booking.RoomType);
        }
    }
}
