using _5DanaUOblacima.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace _5DanaUOblacima
{
    

    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Player>()
                .HasIndex(p => p.Nickname)
                .IsUnique();
        }
    }
}
