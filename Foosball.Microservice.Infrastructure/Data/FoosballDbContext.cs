using System.IO;
using Foosball.Microservice.DomainLogic.AggregateRoot;
using Foosball.Microservice.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Foosball.Microservice.Infrastructure.Data
{
    public class FoosballDbContext : DbContext
    {
        public DbSet<Game> Game { get; set; }

        public FoosballDbContext(DbContextOptions<FoosballDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SetEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GoalEntityConfiguration());
        }
    }
}
