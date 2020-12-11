using System.ComponentModel.DataAnnotations;

namespace News.Contracts.V1.Requests
{
    public class UserUpdateRequest
    {
        public string Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }

        public string Role { get; set; }
    }
}