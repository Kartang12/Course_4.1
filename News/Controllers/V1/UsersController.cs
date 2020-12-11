using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Contracts.V1.Responses;
using News.Data;
using News.Domain;
using News.Services;

namespace News.Controllers.V1
{
    public class UsersController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly UserManager<SMMUser> _userManager;
        private readonly DataContext _context;

        public UsersController(UserManager<SMMUser> userManager, IIdentityService identityService, DataContext context)
        {
            _userManager = userManager;
            _identityService = identityService;
            _context = context;
        }
            
        [HttpGet(ApiRoutes.Users.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var rawUsers = _userManager.Users;
            List<UserDataResponse> response = new List<UserDataResponse>();
            foreach (SMMUser user in rawUsers)
            {
                string role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                response.Add(new UserDataResponse()
                {
                    Id = user.Id,
                    Name = user.UserName,
                    Role = role
                });
            }
            return Ok(response);
        }
        
        [HttpGet(ApiRoutes.Users.Get)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var user = await _identityService.GetUserById(id);
            string role = null;

            role = (await _userManager.GetRolesAsync(user)).First();

            return Ok(new UserDataResponse()
            {
                Id = user.Id,
                Name = user.UserName,
                Role = role,
                Email = user.Email
            });
            // return Ok(await _identityService.GetUserByName(userName));
        }
        
        [HttpPost(ApiRoutes.Users.Add)]
        public async Task<IActionResult> Add([FromBody] UserRegistrationRequest request)
        {
            var registered = await _identityService.RegisterAsync(request.Email, request.Name, request.Password, request.Role, request.Business);
            
            if(registered.Errors == null)
                return Ok();

            return BadRequest(registered.Errors);
        }        
        
        [HttpDelete(ApiRoutes.Users.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string userName)
        {
            var result = await _identityService.DeleteUser(userName);
            
            if(result.Succeeded)
                return Ok();

            if(result.Errors.First().Code == "1")
                return BadRequest(new {error = "You can't delete the last administrator"});
            
            return BadRequest(new {error = "Unable to delete user"});
        }

        [HttpPut(ApiRoutes.Users.Update)]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest request)
        {
            SMMUser user = await _context.Users.FirstOrDefaultAsync(x=> x.Id == request.Id);
            
            if(!String.IsNullOrEmpty(request.Name))
                user.UserName = request.Name;
            if(!String.IsNullOrEmpty(request.Email))
                user.Email = request.Email;

            if(!String.IsNullOrEmpty(request.Role))
            {
                try
                {
                    var currentRole = (await _userManager.GetRolesAsync(user)).First();
                    await _userManager.RemoveFromRoleAsync(user, currentRole);
                }
                catch (Exception e) { }
                await _userManager.AddToRoleAsync(user, request.Role);
            }

            return Ok(_context.SaveChanges() > 0);
        }

        [HttpPut(ApiRoutes.Users.Change)]
        public async Task<IActionResult> Change([FromRoute] string userName, [FromBody] UserSelfUpdateRequest request)
        {
            var user = await _identityService.GetUserByName(userName);

            user.UserName = request.Name;
            user.Email = request.Name;
            
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, request.Password);

            return Ok();
        }
    }
}