using Microsoft.Extensions.Hosting;
using MLSZ.Entities;
using Microsoft.EntityFrameworkCore;

namespace MLSZ.Data
{
    public class MlszContext : DbContext
    {

        public MlszContext(DbContextOptions<MlszContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Field> Fields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}

