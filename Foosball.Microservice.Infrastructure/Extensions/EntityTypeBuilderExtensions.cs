using Foosball.Microservice.DomainLogic.Entities;
using Foosball.Microservice.DomainLogic.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foosball.Microservice.Infrastructure.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static void HasEntityId<T>(this EntityTypeBuilder<T> builder)
            where T : Entity
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasConversion(x => x.Id, guid => EntityId.From(guid));
        }
    }
}
