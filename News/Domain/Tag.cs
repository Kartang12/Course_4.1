using System;
using System.Collections.Generic;

namespace News.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<BusinessType> businessTypes { get; set; }
    }
}