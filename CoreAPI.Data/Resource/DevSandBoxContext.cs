using Microsoft.EntityFrameworkCore;

namespace CoreAPI.Data.Resource
{
    public class DevSandBoxContext : DbContext
    {
        public DevSandBoxContext(DbContextOptions<DevSandBoxContext> options)
            :base(options)
        {
            
        }
    }
}
