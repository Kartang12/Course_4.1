using News.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace News.Contracts.V1.Requests
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public List<BusinessType> Business { get; set; }
    }
}