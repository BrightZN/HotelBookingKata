using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingKata.Tests
{
    public class BookingPolicyServiceTests
    {
        private readonly InMemoryCompanyRepository _companyRepository;

        private readonly BookingPolicyService _sut;

        public BookingPolicyServiceTests()
        {
            _companyRepository = new InMemoryCompanyRepository();

            _sut = new BookingPolicyService(_companyRepository);
        }

        [Fact]
        public async Task SetCompanyPolicyAsync_CompanyWithNoPolicy_AddsPolicy()
        {
            var companyId = new CompanyId("h4ck");

            var existingCompany = new Company(id: companyId);

            _companyRepository.SavedCompany = existingCompany;

            var roomTypes = new List<RoomType> { RoomType.Standard };

            var oldRoomTypes = existingCompany.RoomTypes.ToList();

            await _sut.SetCompanyPolicyAsync(companyId, roomTypes);

            Assert.NotNull(existingCompany.RoomTypes);
        }
    }

    internal class InMemoryCompanyRepository : ICompanyRepository
    {
        public Company SavedCompany { get; internal set; }
    }
}
