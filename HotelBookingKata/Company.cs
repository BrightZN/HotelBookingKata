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
            this.policy = policy;
        }

        public CompanyId Id { get; }

        protected BookingPolicy policy;

        public bool CanBook(RoomType roomType)
        {
            return NoPolicy() || policy.AllowsBookingFor(roomType);
        }

        protected bool NoPolicy() => policy == null;

        public void ChangePolicy(BookingPolicy newPolicy)
        {
            policy = newPolicy;
        }
    }
}