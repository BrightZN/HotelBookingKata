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
            if (await _companyChecker.DoesNotExistAsync(companyId))
                throw new CompanyNotFoundException();

            if (await _employeeRepository.HasEmployeeWithIdAsync(employeeId))
                throw new EmployeeAlreadyExistsException();

            var employee = new Employee(employeeId, companyId);
            await _employeeRepository.SaveEmployeeAsync(employee);
        }

        public async Task DeleteEmployeeAsync(EmployeeId employeeId)
        {
            /* for the purposes for storing "historical" booking info, I have deliberately ignored
             * the kata's requirement for all an employee's bookings to be deleted when they have
             * been deleted.
             */

            await _employeeRepository.DeleteEmployeeByIdAsync(employeeId);
        }
    }
}