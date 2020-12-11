using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace News.Domain
{
    public class BusinessType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<SMMUser> Users { get; set; }

        [JsonIgnore]
        public List<Tag> tags { get; set; }

    }
}