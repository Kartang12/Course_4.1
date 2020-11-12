using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace News.Domain
{
    public class BusinessType
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}