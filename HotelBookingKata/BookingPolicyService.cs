using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingKata
{
    public class BookingPolicyService
    {
        private ICompanyRepository companyRepository;

        public BookingPolicyService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public async Task SetCompanyPolicyAsync(CompanyId companyId, IEnumerable<RoomType> roomTypes)
        {

        }
    }
}