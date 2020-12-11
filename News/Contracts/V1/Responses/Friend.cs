using News.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Contracts.V1.Responses
{
    public class Friend
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public List<BusinessType> business { get; set; }
    }
}
