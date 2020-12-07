using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using News.Contracts.V1.Responses;
using News.Domain;

namespace News.Services
{
    public interface IIdentityService
    {
        Task<AuthSuccessResponse> RegisterAsync(string email, string name, string password, string role, List<BusinessType> businesses);
        
        Task<AuthSuccessResponse> LoginAsync(string email, string password);
        
        Task<SMMUser> GetUserByName(string userName);

        Task<IdentityResult> DeleteUser(string name);
    }
}