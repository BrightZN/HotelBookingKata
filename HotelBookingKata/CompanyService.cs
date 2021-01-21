using System;
using System.Threading.Tasks;

namespace HotelBookingKata
{
    public class CompanyService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CompanyService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task AddEmployeeAsync(CompanyId companyId, EmployeeId employeeId)
        {
            if (await _employeeRepository.HasEmployeeWithIdAsync(employeeId))
                throw new EmployeeAlreadyExistsException();

            var employee = new Employee(employeeId, companyId);

            await _employeeRepository.AddEmployeeAsync(employee);
        }

        public async Task DeleteEmployeeAsync(EmployeeId employeeId)
        {
            await _employeeRepository.DeleteEmployeeByIdAsync(employeeId);
        }
    }
}