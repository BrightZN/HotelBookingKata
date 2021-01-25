using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingKata
{
    public class BookingPolicyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BookingPolicyService(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository)
        {
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task SetCompanyPolicyAsync(CompanyId companyId, IEnumerable<RoomType> roomTypes)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(companyId);

            company.ChangePolicy(roomTypes);

            await _companyRepository.SaveCompanyAsync(company);
        }

        public async Task SetEmployeePolicyAsync(EmployeeId employeeId, IEnumerable<RoomType> roomTypes)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            employee.ChangePolicy(roomTypes);

            await _employeeRepository.SaveEmployeeAsync(employee);
        }

        public async Task<bool> IsBookingAllowedAsync(EmployeeId employeeId, RoomType roomType)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            var company = await _companyRepository.GetCompanyByIdAsync(employee.CompanyId);

            return employee.CanBook(roomType) || company.CanBook(roomType);
        }
    }
}