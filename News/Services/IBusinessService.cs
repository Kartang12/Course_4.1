using News.Contracts.V1.Requests;
using News.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Services
{
    public interface IBusinessService
    {
        Task<List<BusinessType>> GetAllBusinesseAsync();

        Task<bool> CreateBusinessAsync(string bName, List<Tag> tags);

        Task<BusinessType> GetBusinessByIdAsync(string bId);

        Task<BusinessType> GetBusinessByNameAsync(string bName);

        Task<bool> DeleteBusinessAsync(string id);

        Task<List<BusinessType>> GetBusinessOfUser(string userId);
        Task<bool> UpdateBusinessAsync(string bId, CreateBusinessRequest request);
    }
}
