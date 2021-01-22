using System.Threading.Tasks;

namespace HotelBookingKata
{
    public interface ICompanyRepository
    {
        Task SaveCompanyAsync(Company company);
        
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="CompanyNotFoundException"></exception>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Task<Company> GetCompanyByIdAsync(CompanyId companyId);
    }
}