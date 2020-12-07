using News.Domain;
using System.Collections.Generic;

namespace News.Contracts.V1.Requests
{
    public class CreateBusinessRequest
    {
        public string Name { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
