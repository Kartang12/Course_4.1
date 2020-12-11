using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Domain
{
    public class SMMUser: IdentityUser
    {
        public List<BusinessType> businessTypes{ get; set; }
        
        public virtual List<SMMUser> friends{ get; set; }
    }
}
