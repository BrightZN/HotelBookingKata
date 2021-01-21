using System.Threading.Tasks;

namespace HotelBookingKata
{
    public interface ICompanyChecker
    {
        Task<bool> DoesNotExistAsync(CompanyId companyId);
    }
}