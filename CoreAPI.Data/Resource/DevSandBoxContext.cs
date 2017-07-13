using CoreAPI.Domain.Dto;
using Microsoft.EntityFrameworkCore;

namespace CoreAPI.Data.Resource
{
    public class DevSandBoxContext : DbContext
    {
        public DevSandBoxContext(DbContextOptions<DevSandBoxContext> options)
            : base(options)
        {

        }

        public DbSet<BillingItem> BillingItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillingItem>().HasKey(k => new { k.Id });
        }
    }
}
