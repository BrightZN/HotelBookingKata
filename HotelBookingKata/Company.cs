using System;
using System.Linq;

namespace HotelBookingKata
{
    public class Company : BookingAgent
    {
        public Company(CompanyId id)
            : this(id, null)
        {

        }

        public Company(CompanyId id, BookingPolicy policy)
            : base(policy)
        {
            Id = id;
        }

        public CompanyId Id { get; }
    }
}