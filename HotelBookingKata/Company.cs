using System;
using System.Linq;

namespace HotelBookingKata
{
    public class Company
    {
        public Company(CompanyId id)
        {
            Id = id;
        }

        public Company(CompanyId id, BookingPolicy policy)
            : this(id)
        {
            _policy = policy;
        }

        public CompanyId Id { get; }

        private BookingPolicy _policy;

        public bool CanBook(RoomType roomType)
        {
            return NoPolicy() || _policy.AllowsBookingFor(roomType);
        }

        private bool NoPolicy() => _policy == null;

        public void ChangePolicy(BookingPolicy newPolicy)
        {
            _policy = newPolicy;
        }
    }
}