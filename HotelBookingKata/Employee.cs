namespace HotelBookingKata
{
    public class Employee
    {
        public Employee(EmployeeId id, CompanyId companyId)
        {
            Id = id;
            CompanyId = companyId;
        }

        public EmployeeId Id { get; }
        public CompanyId CompanyId { get; }
    }
}