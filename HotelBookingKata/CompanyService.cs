using System;
using System.Threading.Tasks;

namespace HotelBookingKata
{
    public class CompanyService
    {
        private readonly ICompanyChecker _companyChecker;
        private readonly IEmployeeRepository _employeeRepository;

        public CompanyService(ICompanyChecker companyChecker, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _companyChecker = companyChecker;
        }

        public async Task AddEmployeeAsync(CompanyId companyId, EmployeeId employeeId)
        {
            // could a Company class be treated as an Aggregate?

            if (await _companyChecker.DoesNotExistAsync(companyId))
                throw new CompanyNotFoundException();

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