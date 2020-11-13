using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using News.Domain;

namespace News.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string name, string password, string role, string business);
        
        Task<AuthenticationResult> LoginAsync(string email, string password);
        
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        
        Task<IdentityUser> GetUserByName(string userName);

        Task<IdentityResult> DeleteUser(string name);
    }
}