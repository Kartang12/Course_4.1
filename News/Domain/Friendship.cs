using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace News.Domain
{
    public class Friendship
    {
        [Key]
        public string id { get; set; }
        public string userId { get; set; }
        public string friendId { get; set; }
    }
}
