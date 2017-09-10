using CoreAPI.Domain.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CoreAPI.Data.Resource
{
    public class DevSandBoxContext : DbContext
    {
       

        public DbSet<BillingItem> BillingItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillingItem>().HasKey(k => new { k.Id });
        }
    }
}
