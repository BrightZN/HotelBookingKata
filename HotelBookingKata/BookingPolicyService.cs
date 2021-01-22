using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingKata
{
    public class BookingPolicyService
    {
        private readonly ICompanyRepository _companyRepository;

        public BookingPolicyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task SetCompanyPolicyAsync(CompanyId companyId, IEnumerable<RoomType> roomTypes)
        {
            //await _companyRepository.SaveCompanyAsync(new C);
        }
    }
}