using System;
using System.Threading.Tasks;

namespace HotelBookingKata
{
    public class BookingService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly BookingPolicyService _bookingPolicyService;
        private readonly IBookingScheduleProvider _bookingScheduleProvider;
        private readonly IBookingIdGenerator _bookingIdGenerator;

        public BookingService(
            IHotelRepository hotelRepository,
            BookingPolicyService bookingPolicyService,
            IBookingScheduleProvider bookingScheduleProvider, 
            IBookingIdGenerator bookingIdGenerator)
        {
            _hotelRepository = hotelRepository;
            _bookingPolicyService = bookingPolicyService;
            _bookingScheduleProvider = bookingScheduleProvider;
            _bookingIdGenerator = bookingIdGenerator;
        }

        public async Task<Booking> BookAsync(EmployeeId employeeId, HotelId hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
        {
            if (checkOut < checkIn.AddDays(1))
                throw new BookingCheckInDateException();

            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);

            if (hotel.DoesNotOffer(roomType))
                throw new BookingHotelDoesNotOfferRoomTypeException();

            if (await NoBookingsAllowedFor(employeeId, roomType))
                throw new BookingPolicyException();

            var bookingSchedule = await _bookingScheduleProvider.GetBookingScheduleAsync(hotelId, roomType, checkIn, checkOut);

            if (bookingSchedule.CannotAccomodateBookingFor(roomType, checkIn, checkOut))
                throw new BookingRoomTypeNotAvailable();

            var bookingId = await _bookingIdGenerator.GenerateBookingIdAsync();

            var booking = new Booking(bookingId, hotelId, employeeId, roomType, checkIn, checkOut);

            return booking;
        }

        private async Task<bool> NoBookingsAllowedFor(EmployeeId employeeId, RoomType roomType)
        {
            return !await _bookingPolicyService.IsBookingAllowedAsync(employeeId, roomType);
        }
    }
}