using krediku_be.Models;
using Microsoft.EntityFrameworkCore;

namespace krediku_be.Data
{
    public class KredikuContext: DbContext
    {
        public KredikuContext(DbContextOptions<KredikuContext> opt): base(opt)
        {
            
        }

        public DbSet<Location> locations { get; set; }
        public DbSet<Transaction> transactions { get; set; }
    }
}
