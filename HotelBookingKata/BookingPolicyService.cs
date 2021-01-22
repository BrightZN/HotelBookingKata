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

            var bookingPolicy = new BookingPolicy(roomTypes);

            company.ChangePolicy(bookingPolicy);

            await _companyRepository.SaveCompanyAsync(company);
        }

        public async Task SetEmployeePolicyAsync(EmployeeId employeeId, IEnumerable<RoomType> roomTypes)
        {

        }
    }
}