using Foosball.Microservice.DomainLogic.Entities;
using Foosball.Microservice.DomainLogic.ValueObjects;
using Xunit;

namespace Foosball.Microservice.DomainLogic.UnitTests
{
    public class SetTests
    {
        [Fact]
        public void GivenNewSet_WhenScoringTenGoalsForTeamA_ThenTeamAWinsTheSet()
        {
            // arrange
            var set = Set.Create(SetOrder.CreateFrom(0));

            // act
            for (var i = 0; i < 9; i++)
            {
                set.ScoreGoal(true);
                Assert.False(set.TeamAWon);
                Assert.False(set.TeamBWon);
                Assert.False(set.IsFinished);
            }

            set.ScoreGoal(true);

            // assert
            Assert.True(set.TeamAWon);
            Assert.False(set.TeamBWon);
            Assert.True(set.IsFinished);
        }

        [Fact]
        public void GivenNewSet_WhenScoringTenGoalsForTeamB_ThenTeamBWinsTheSet()
        {
            // arrange
            var set = Set.Create(SetOrder.CreateFrom(0));

            // act
            for (var i = 0; i < 9; i++)
            {
                set.ScoreGoal(false);
                Assert.False(set.TeamAWon);
                Assert.False(set.TeamBWon);
                Assert.False(set.IsFinished);
            }

            set.ScoreGoal(false);

            // assert
            Assert.False(set.TeamAWon);
            Assert.True(set.TeamBWon);
            Assert.True(set.IsFinished);
        }

        [Fact]
        public void GivenNewSet_WhenScoringTenGoalsForTeamAAndNineGoalsForTeamB_ThenTeamAWinsTheSet()
        {
            // arrange
            var set = Set.Create(SetOrder.CreateFrom(0));

            // act
            for (var i = 0; i < 9; i++)
            {
                set.ScoreGoal(true);
                set.ScoreGoal(false);
                Assert.False(set.TeamAWon);
                Assert.False(set.TeamBWon);
                Assert.False(set.IsFinished);
            }

            set.ScoreGoal(true);

            // assert
            Assert.True(set.TeamAWon);
            Assert.False(set.TeamBWon);
            Assert.True(set.IsFinished);
        }
    }
}
