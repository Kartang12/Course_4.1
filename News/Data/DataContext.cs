using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using News.Domain;

namespace News.Data
{
    public class DataContext : IdentityDbContext<SMMUser, IdentityRole, string>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        public DbSet<BusinessType> Businesses{ get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Tag>().HasKey(x => new {x.TagId, x.TagName});
            //builder.Entity<UserBusiness>().HasNoKey();
        }
    }
}
