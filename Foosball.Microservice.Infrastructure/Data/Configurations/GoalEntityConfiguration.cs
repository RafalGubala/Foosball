using Foosball.Microservice.DomainLogic.Entities;
using Foosball.Microservice.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foosball.Microservice.Infrastructure.Data.Configurations
{
    public class GoalEntityConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.HasEntityId();

            builder
                .Property(x => x.ScoreAt)
                .IsRequired();
        }
    }
}
