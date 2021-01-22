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
            _policy = policy;
        }

        public EmployeeId Id { get; }
        public CompanyId CompanyId { get; }

        private BookingPolicy _policy;

        public bool CanBook(RoomType roomType)
        {
            return NoPolicy() || _policy.AllowsBookingFor(roomType);
        }

        private bool NoPolicy() => _policy == null;
    }
}