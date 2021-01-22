using System.Threading.Tasks;

namespace HotelBookingKata
{
    public interface ICompanyRepository
    {
        Task SaveCompanyAsync(Company company);
    }
}