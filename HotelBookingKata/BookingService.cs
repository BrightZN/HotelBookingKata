using System;
using System.Threading.Tasks;

namespace HotelBookingKata
{
    public class BookingService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly BookingPolicyService _bookingPolicyService;

        public BookingService(IHotelRepository hotelRepository, BookingPolicyService bookingPolicyService)
        {
            _hotelRepository = hotelRepository;
            _bookingPolicyService = bookingPolicyService;
        }

        public async Task BookAsync(EmployeeId employeeId, HotelId hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
        {
            if(checkOut < checkIn.AddDays(1))
                throw new BookingCheckInDateException();

            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);

            if(hotel.DoesNotOffer(roomType))
                throw new BookingHotelDoesNotOfferRoomTypeException();

            //if(await _bookingPolicyService.IsBookingAllowedAsync(employeeId, roomType))

            throw new BookingPolicyException();
        }
    }
}