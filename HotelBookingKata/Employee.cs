using System;

namespace HotelBookingKata
{
    public class Employee
    {

        public Employee(EmployeeId id, CompanyId companyId)
        {
            Id = id;
            CompanyId = companyId;
        }

        public Employee(EmployeeId id, CompanyId companyId, BookingPolicy policy) 
            : this(id, companyId)
        {
            this.policy = policy;
        }

        public EmployeeId Id { get; }
        public CompanyId CompanyId { get; }

        protected BookingPolicy policy;

        public bool CanBook(RoomType roomType)
        {
            return NoPolicy() || policy.AllowsBookingFor(roomType);
        }

        protected bool NoPolicy() => policy == null;
    }
}