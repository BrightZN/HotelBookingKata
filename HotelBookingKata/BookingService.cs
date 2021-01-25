using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingKata
{
    public class BookingService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly BookingPolicyService _bookingPolicyService;
        private readonly IBookingRepository _bookingRepository;

        public BookingService(
            IHotelRepository hotelRepository,
            BookingPolicyService bookingPolicyService,
            IBookingRepository bookingRepository)
        {
            _hotelRepository = hotelRepository;
            _bookingPolicyService = bookingPolicyService;
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking> BookAsync(EmployeeId employeeId, HotelId hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
        {
            if (CheckOutIsTooEarly(checkIn, checkOut))
                throw new BookingCheckOutDateException();

            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);

            if (hotel.DoesNotOffer(roomType))
                throw new BookingHotelDoesNotOfferRoomTypeException();

            if (await NoBookingsAllowedFor(employeeId, roomType))
                throw new BookingPolicyException();

            var bookings = await _bookingRepository.GetBookingsAsync(hotelId, roomType, checkIn, checkOut);

            var bookingSchedule = new BookingSchedule(hotel, bookings);

            if (bookingSchedule.CannotAccomodateBookingFor(roomType, checkIn, checkOut))
                throw new BookingRoomTypeNotAvailable();

            var booking = CreateBooking(employeeId, hotelId, roomType, checkIn, checkOut);

            await _bookingRepository.SaveBookingAsync(booking);

            return booking;
        }

        private static bool CheckOutIsTooEarly(DateTime checkIn, DateTime checkOut)
        {
            return checkOut < checkIn.AddDays(1);
        }

        private static Booking CreateBooking(EmployeeId employeeId, HotelId hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
        {
            var bookingId = new BookingId(Guid.NewGuid());

            var booking = new Booking(bookingId, hotelId, employeeId, roomType, checkIn, checkOut);
            return booking;
        }

        private async Task<bool> NoBookingsAllowedFor(EmployeeId employeeId, RoomType roomType)
        {
            return !await _bookingPolicyService.IsBookingAllowedAsync(employeeId, roomType);
        }
    }
}