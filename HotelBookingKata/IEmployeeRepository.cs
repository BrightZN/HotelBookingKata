using System.Threading.Tasks;

namespace HotelBookingKata
{
    public interface IEmployeeRepository
    {
        Task AddEmployeeAsync(Employee employee);
        Task<bool> HasEmployeeWithIdAsync(EmployeeId employeeId);
        Task DeleteEmployeeByIdAsync(EmployeeId employeeId);
    }
}