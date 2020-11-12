using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace News.Domain
{
    public class Tag
    {
        [Key]
        public Guid TagId { get; set; }

        [ForeignKey(nameof(TagName))]
        public string TagName { get; set; }
    }
}