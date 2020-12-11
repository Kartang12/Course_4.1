using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using News.Contracts.V1.Responses;
using News.Data;
using News.Domain;
using News.Options;

namespace News.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<SMMUser> _userManager;
        private readonly DataContext _context;
        private readonly IBusinessService _businessService;
        
        public IdentityService(
            UserManager<SMMUser> userManager, 
            JwtSettings jwtSettings, 
            TokenValidationParameters tokenValidationParameters, 
            DataContext context, 
            RoleManager<IdentityRole> roleManager,
            IBusinessService businessService
            )
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _businessService = businessService;
        }
        
        public async Task<AuthSuccessResponse> RegisterAsync(string email, string name, string password, string role, List<BusinessType> business)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return new AuthSuccessResponse
                {
                    Errors = new[] {"email занят"}
                };
            }

            var newUserId = Guid.NewGuid();
            var newUser = new SMMUser
            {
                Id = newUserId.ToString(),
                Email = email,
                UserName = name,
            };
            
            if (!_roleManager.Roles.Select(x => x.Name.ToLower() == role.ToLower()).Any())
                return new AuthSuccessResponse
                {
                    Success = false,
                    Errors = new[] { "Такой роли не существует" }
                };

            var createdUser = await _userManager.CreateAsync(newUser, password);
            await _context.SaveChangesAsync();

            if (!createdUser.Succeeded)
            {
                return new AuthSuccessResponse
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }
            
            newUser = await _userManager.FindByEmailAsync(email);
            await _userManager.AddToRoleAsync(newUser, role);

            var newBusinesses = new List<BusinessType>();

            foreach(var b in business)
            {
                newBusinesses.Add(await _businessService.GetBusinessByIdAsync(b.Id.ToString()));
            }

            newUser.businessTypes = newBusinesses;

            await _context.SaveChangesAsync();

            return new AuthSuccessResponse()
            {
                Id = newUser.Id,
                Email = newUser.Email,
                Name = newUser.UserName,
                Success = true
            };
        }
        
        public async Task<AuthSuccessResponse> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthSuccessResponse
                {
                    Errors = new[] {"User does not exist"}
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthSuccessResponse
                {
                    Errors = new[] {"User/password combination is wrong"}
                };
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            return new AuthSuccessResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.UserName,
                Role = userRoles[0],
                Success = true
            };
        }

        public async Task<SMMUser> GetUserByName(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }
        public async Task<SMMUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        
        public async Task<IdentityResult> DeleteUser(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            try
            {
                if ((await _userManager.GetRolesAsync(user)).First().ToLower() == "admin")
                {
                    if ((await _userManager.GetUsersInRoleAsync("Admin")).Count <= 1)
                    {
                        return IdentityResult.Failed(new IdentityError()
                        {
                            Code = "1",
                            Description = "You can't delete the only administrator"
                        });
                    }
                }
            }catch(Exception ex){}
            
            return await _userManager.DeleteAsync(user);
        }
    }
}