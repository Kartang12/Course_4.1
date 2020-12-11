using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Contracts.V1.Responses;
using News.Data;
using News.Domain;
using News.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Controllers.V1
{
    public class FriendsController : Controller
    {
        private readonly UserManager<SMMUser> _userManager;
        private readonly DataContext _context;
        public FriendsController(UserManager<SMMUser> userManager, DataContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet(ApiRoutes.Friends.GetPeople)]
        public async Task<IActionResult> GetPeople()
        {
            var users = _context.Users.Include(x => x.businessTypes);

            List<Friend> response = new List<Friend>();
            foreach (var friend in users)
            {
                if (friend.businessTypes == null || friend.businessTypes.Count == 0)
                    continue;
                response.Add(new Friend()
                {
                    id = friend.Id,
                    name = friend.UserName,
                    email = friend.Email,
                    business = friend.businessTypes
                });
            }

            return Ok(response);
        }        
        
        [HttpGet(ApiRoutes.Friends.GetAll)]
        public async Task<IActionResult> GetAll([FromRoute] string id)
        {
            var user = await _context.Users.Include(x => x.businessTypes).Include(x=>x.friends).FirstOrDefaultAsync(x=> x.Id == id);

            List<Friend> response = new List<Friend>();
            if(user.friends.Count > 0)
                foreach (var friend in user.friends)
                {
                    var fr = await _context.Users.Include(x => x.businessTypes).FirstAsync(x => x.Id == friend.Id);
                    response.Add(new Friend()
                    {
                        id = fr.Id,
                        name = fr.UserName,
                        email = fr.Email,
                        business = fr.businessTypes
                    });
                }

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Friends.Search)]
        public async Task<IActionResult> Search([FromRoute] string business)
        {
            var users = _context.Users.Include(x=> x.businessTypes);
            List<Friend> friends = new List<Friend>();
            foreach (var user in users) 
            {
                if (user.businessTypes == null || user.businessTypes.Count < 1)
                    continue;
                if(user.businessTypes[0].Name == business)
                    friends.Add(new Friend()
                    {
                        id = user.Id,
                        email = user.Email,
                        name = user.UserName,
                        business = user.businessTypes
                    });
            }
            return Ok(friends);
        }

        [HttpPost(ApiRoutes.Friends.Add)]
        public async Task<IActionResult> Add([FromBody] FriendRequest request)
        {
            var user = await _context.Users.Include(x => x.friends).FirstOrDefaultAsync( x=> x.Id == request.userId);
            var friend = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.friendId);

            if (user.friends == null)
                user.friends = new List<SMMUser>();
            user.friends.Add(friend);

            return Ok(_context.SaveChanges() > 0);
        }
       
        [HttpDelete(ApiRoutes.Friends.Delete)]
        public async Task<IActionResult> Delete([FromBody] FriendRequest request)
        {
            var user = _context.Users.Include(x=>x.friends).FirstOrDefault(x=> x.Id == request.userId);
            var friend = _context.Users.Include(x => x.friends).FirstOrDefault(x => x.Id == request.friendId);
            user.friends.Remove(friend);
            return Ok(_context.SaveChanges() > 0);
        }
    }
}
