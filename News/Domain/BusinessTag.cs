using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Domain
{
    
    public class BusinessTag
    {
        public Guid businessId { get; set; }
        public Guid tagId { get; set; }
    }
}
