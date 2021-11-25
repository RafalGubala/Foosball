using Foosball.Microservice.DomainLogic.ValueObjects;
using System;

namespace Foosball.Microservice.DomainLogic.Entities
{
    public class Goal : Entity
    {
        private Goal(EntityId id, DateTime scoreAt)
            : base(id)
        {
            ScoreAt = scoreAt;
        }

        public DateTime ScoreAt { get; }

        public static Goal Create() => new Goal(EntityId.New(), DateTime.UtcNow);
    }
}
