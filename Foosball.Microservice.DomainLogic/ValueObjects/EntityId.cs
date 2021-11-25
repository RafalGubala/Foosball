using System;
using System.Collections.Generic;

namespace Foosball.Microservice.DomainLogic.ValueObjects
{
    public class EntityId : ValueObject
    {
        private EntityId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public static EntityId New() => new EntityId(Guid.NewGuid());

        public static EntityId From(Guid id) => new EntityId(id);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
