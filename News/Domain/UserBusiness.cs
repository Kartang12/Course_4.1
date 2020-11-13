using System.ComponentModel.DataAnnotations;

namespace News.Domain
{
    public class UserBusiness
    {
        [Key]
        public string userId { get; set; }
        public string sphereId { get; set; }
    }
}
