using System.Threading.Tasks;

namespace HotelBookingKata.Tests
{
    internal class InMemoryEmployeeRepository : IEmployeeRepository
    {
        public Employee SavedEmployee { get; internal set; }

        public Task AddEmployeeAsync(Employee employee)
        {
            SavedEmployee = employee;

            return Task.CompletedTask;
        }

        public Task DeleteEmployeeByIdAsync(EmployeeId employeeId)
        {
            if (SavedEmployee != null && SavedEmployee.Id == employeeId)
            {
                SavedEmployee = null;

                return Task.CompletedTask;
            }

            throw new EmployeeNotFoundException();
        }

        public Task<bool> HasEmployeeWithIdAsync(EmployeeId employeeId)
        {
            return Task.FromResult(SavedEmployee != null && SavedEmployee.Id == employeeId);
        }
    }
}
