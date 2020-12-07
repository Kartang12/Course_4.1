using System.Collections.Generic;

namespace News.Contracts.V1.Responses
{
    public class AuthSuccessResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}