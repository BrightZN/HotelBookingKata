using System;
using System.Linq;

namespace HotelBookingKata
{
    public class Company : BookingAgent
    {
        public Company(CompanyId id, BookingPolicy policy = null)
            : base(policy)
        {
            Id = id;
        }

        public CompanyId Id { get; }
    }
}