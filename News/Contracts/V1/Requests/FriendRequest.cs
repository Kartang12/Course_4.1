using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Contracts.V1.Requests
{
    public class FriendRequest
    {
        public string userId { get; set; }
        public string friendId { get; set; }
    }
}
