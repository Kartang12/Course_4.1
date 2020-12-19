using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace News.Domain
{
    public class Message
    {
        public string fromUser { get; set; }
        public string toUser { get; set; }
        public string content { get; set; }
    }
}
