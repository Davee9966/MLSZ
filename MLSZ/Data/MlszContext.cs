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

        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(et => {
                et.HasKey(e => e.Id);
                et.Property(e => e.Username).IsRequired();
                et.Property(e => e.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<User>().HasData(
                new User("admin")
                );
        }
    }
}

