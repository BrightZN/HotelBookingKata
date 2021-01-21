using System.Threading.Tasks;

namespace HotelBookingKata.Tests
{
    internal class InMemoryCompanyChecker : ICompanyChecker
    {
        public CompanyId CompanyId { get; set;  }

        public Task<bool> DoesNotExistAsync(CompanyId companyId)
        {
            return Task.FromResult(CompanyId != companyId);
        }
    }
}
