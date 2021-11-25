using Foosball.Microservice.DomainLogic.ValueObjects;

namespace Foosball.Microservice.DomainLogic.Entities
{
    public abstract class Entity
    {
        protected Entity(EntityId id)
        {
            Id = id;
        }

        public EntityId Id { get; }
    }
}
