using System;
using System.Collections.Generic;

namespace News.Domain
{
    public class BusinessType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<SMMUser> Users { get; set; }
        public List<Tag> tags { get; set; }

    }
}