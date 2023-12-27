using DeutschDeck.WebAPI.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeutschDeck.WebAPI.Database
{
    public class DDContext(DbContextOptions<DDContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<UserEntity>().HasIndex(u => u.Email).IsUnique();
        }
    }
}
