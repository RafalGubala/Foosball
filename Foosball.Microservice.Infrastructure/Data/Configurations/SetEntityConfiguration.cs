using Foosball.Microservice.DomainLogic.Entities;
using Foosball.Microservice.DomainLogic.ValueObjects;
using Foosball.Microservice.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foosball.Microservice.Infrastructure.Data.Configurations
{
    public class SetEntityConfiguration : IEntityTypeConfiguration<Set>
    {
        public void Configure(EntityTypeBuilder<Set> builder)
        {
            builder.HasEntityId();

            builder
                .Ignore(x => x.IsFinished)
                .Ignore(x => x.TeamAWon)
                .Ignore(x => x.TeamBWon);

            builder
                .Property(x => x.Order)
                .HasConversion(x => x.Value, i => SetOrder.CreateFrom(i));

            builder
                .HasMany(x => x.TeamAGoals)
                .WithOne()
                .HasForeignKey("TeamASetId");

            builder
                .HasMany(x => x.TeamBGoals)
                .WithOne()
                .HasForeignKey("TeamBSetId");

            builder
                .Navigation(x => x.TeamAGoals)
                .AutoInclude();

            builder
                .Navigation(x => x.TeamBGoals)
                .AutoInclude();
        }
    }
}
