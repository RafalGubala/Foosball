using Foosball.Microservice.DomainLogic.ValueObjects;
using System.Collections.Generic;

namespace Foosball.Microservice.DomainLogic.Entities
{
    public class Set : Entity
    {
        private Set(EntityId id, SetOrder order, List<Goal> teamAGoals, List<Goal> teamBGoals)
            : base(id)
        {
            Order = order;
            TeamAGoals = teamAGoals;
            TeamBGoals = teamBGoals;
        }

        private Set(EntityId id, SetOrder order)
            : this(id, order, new List<Goal>(), new List<Goal>())
        {
        }

        public SetOrder Order { get; }

        public List<Goal> TeamAGoals { get; }

        public List<Goal> TeamBGoals { get; }

        public bool IsFinished => TeamAWon || TeamBWon;

        public bool TeamAWon => TeamAGoals.Count == 10;

        public bool TeamBWon => TeamBGoals.Count == 10;

        internal void ScoreGoal(bool scoreForTeamA)
        {
            if (scoreForTeamA)
            {
                TeamAGoals.Add(Goal.Create());
                return;
            }

            TeamBGoals.Add(Goal.Create());
        }

        public static Set Create(SetOrder order) => new Set(EntityId.New(), order);
    }
}
