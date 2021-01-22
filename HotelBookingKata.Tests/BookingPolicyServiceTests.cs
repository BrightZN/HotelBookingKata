using System;
using System.Collections.Generic;
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
        public async Task SetCompanyPolicyAsync_ExistingCompanyWithoutPolicy_AddsPolicy()
        {
            var companyId = new CompanyId("h4ck");

            var roomTypes = new List<RoomType> { RoomType.Standard };

            var existingCompany = new Company(companyId);

            _companyRepository.SavedCompany = existingCompany;

            await _sut.SetCompanyPolicyAsync(companyId, roomTypes);

            Assert.True(existingCompany.CanBook(RoomType.Standard));
            Assert.False(existingCompany.CanBook(RoomType.Presidential));
        }
    }

    internal class InMemoryCompanyRepository : ICompanyRepository
    {
        public Company SavedCompany { get; internal set; }

        public Task<Company> GetCompanyByIdAsync(CompanyId companyId)
        {
            if (SavedCompany != null && SavedCompany.Id == companyId)
                return Task.FromResult(SavedCompany);

            throw new CompanyNotFoundException();
        }

        public Task SaveCompanyAsync(Company company)
        {
            SavedCompany = company;

            return Task.CompletedTask;
        }
    }
}
