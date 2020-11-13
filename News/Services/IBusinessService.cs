using News.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Services
{
    public interface IBusinessService
    {
        Task<List<BusinessType>> GetAllBusinesseAsync();

        Task<bool> CreateBusinessAsync(string bName);

        Task<BusinessType> GetBusinessByNameAsync(string bName);

        Task<bool> DeleteBusinessAsync(string bName);

        Task<BusinessType> GetBusinessOfUser(string userId);
    }
}
