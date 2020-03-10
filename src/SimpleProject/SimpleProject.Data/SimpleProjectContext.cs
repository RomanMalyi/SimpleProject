using Microsoft.EntityFrameworkCore;
using SimpleProject.Data.Models;

namespace SimpleProject.Data
{
    public class SimpleProjectContext : DbContext
    {
        public SimpleProjectContext(DbContextOptions<SimpleProjectContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Entity> Entities { get; set; }
    }
}
