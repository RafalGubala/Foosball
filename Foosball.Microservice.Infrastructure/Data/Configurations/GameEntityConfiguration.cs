using Foosball.Microservice.DomainLogic.AggregateRoot;
using Foosball.Microservice.DomainLogic.ValueObjects;
using Foosball.Microservice.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foosball.Microservice.Infrastructure.Data.Configurations
{
    public class GameEntityConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasEntityId();

            builder
                .Property(x => x.IsFinished)
                .IsRequired();

            builder
                .Property(x => x.CreatedAt)
                .IsRequired();

            builder
                .Property(x => x.TeamA)
                .HasConversion(teamName => teamName.Name, s => TeamName.Create(s));

            builder
                .Property(x => x.TeamB)
                .HasConversion(teamName => teamName.Name, s => TeamName.Create(s));

            builder
                .HasMany(x => x.Sets)
                .WithOne();

            builder
                .Navigation(x => x.Sets)
                .AutoInclude();
        }
    }
}
