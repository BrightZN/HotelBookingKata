using System;

namespace HotelBookingKata
{
    public class Employee : BookingAgent
    {
        public Employee(EmployeeId id, CompanyId companyId, BookingPolicy policy = null) 
            : base(policy)
        {
            Id = id;
            CompanyId = companyId;
        }

        public EmployeeId Id { get; }
        public CompanyId CompanyId { get; }
    }
}