using Foosball.Microservice.DomainLogic.Entities;
using Foosball.Microservice.DomainLogic.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foosball.Microservice.DomainLogic.AggregateRoot
{
    public class Game : Entity, IAggregateRoot
    {
        private Game(EntityId id, TeamName teamA, TeamName teamB, List<Set> sets, DateTime createdAt)
            : base(id)
        {
            TeamA = teamA;
            TeamB = teamB;
            Sets = sets;
            CreatedAt = createdAt;
        }

        private Game(EntityId id, TeamName teamA, TeamName teamB, DateTime createdAt)
            : this(id, teamA, teamB, new List<Set>(), createdAt)
        {
        }

        public TeamName TeamA { get; }

        public TeamName TeamB { get; }

        public List<Set> Sets { get; }

        public bool IsFinished { get; private set; }

        public DateTime CreatedAt { get; }

        public void ScoreGoal(TeamName team)
        {
            if (IsFinished)
            {
                throw new InvalidOperationException("Game is finished and cannot be modified");
            }

            if (!TeamA.Equals(team) && !TeamB.Equals(team))
            {
                throw new InvalidOperationException("No team found for passed team name");
            }

            var currentSet = Sets.OrderBy(x => x.Order).First(x => !x.IsFinished);
            currentSet.ScoreGoal(team.Equals(TeamA));

            if (Sets.Count(x => x.TeamAWon) == 2 ||
                Sets.Count(x => x.TeamBWon) == 2)
            {
                IsFinished = true;
            }
        }

        public static Game Create(EntityId id, TeamName teamA, TeamName teamB)
        {
            var sets = new List<Set>
            {
                Set.Create(SetOrder.CreateFrom(0)),
                Set.Create(SetOrder.CreateFrom(1)),
                Set.Create(SetOrder.CreateFrom(2))
            };
            return new Game(id, teamA, teamB, sets, DateTime.UtcNow);
        }
    }
}
