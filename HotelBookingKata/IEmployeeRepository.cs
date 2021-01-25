using System.Threading.Tasks;

namespace HotelBookingKata
{
    public interface IEmployeeRepository
    {
        Task SaveEmployeeAsync(Employee employee);
        Task<bool> HasEmployeeWithIdAsync(EmployeeId employeeId);
        Task DeleteEmployeeByIdAsync(EmployeeId employeeId);
        Task<Employee> GetEmployeeByIdAsync(EmployeeId employeeId);
    }
}