using System;
using Foosball.Microservice.DomainLogic.ValueObjects;
using Xunit;

namespace Foosball.Microservice.DomainLogic.UnitTests
{
    public class GameAggregateRootTests
    {
        [Fact]
        public void GivenFinishedGame_WhenScoringNewGoal_ThenExceptionIsThrown()
        {
            // arrange
            var game = Stubs.GetFinishedGame();

            // act & assert
            Assert.True(game.IsFinished);
            Assert.Throws<InvalidOperationException>(() => game.ScoreGoal(Stubs.TeamANameStub));
        }

        [Fact]
        public void GivenAnyGame_WhenScoringNewGoalForTeamThatIsOutsideOfGame_ThenExceptionIsThrown()
        {
            // arrange
            var game = Stubs.GetNewGame();
            var unknownTeamName = TeamName.Create("Unknown Team");

            // act & assert
            Assert.Throws<InvalidOperationException>(() => game.ScoreGoal(unknownTeamName));
        }

        [Fact]
        public void GivenNewGame_WhenOneTeamWinsTwoSetsInARow_ThenGameIsEnded()
        {
            // arrange
            var game = Stubs.GetNewGame();

            // act
            for (var i = 0; i < 19; i++)
            {
                game.ScoreGoal(Stubs.TeamANameStub);
                Assert.False(game.IsFinished);
            }

            game.ScoreGoal(Stubs.TeamANameStub);

            // assert
            Assert.True(game.IsFinished);
        }

        [Fact]
        public void GivenNewGame_WhenOneTeamWinsTwoSetsAndSecondTeamWinsOneSet_ThenGameIsEnded()
        {
            // arrange
            var game = Stubs.GetNewGame();

            // act
            for (var i = 0; i < 10; i++)
            {
                game.ScoreGoal(Stubs.TeamANameStub);
            }
            Assert.False(game.IsFinished);

            for (var i = 0; i < 10; i++)
            {
                game.ScoreGoal(Stubs.TeamBNameStub);
            }
            Assert.False(game.IsFinished);

            for (var i = 0; i < 10; i++)
            {
                game.ScoreGoal(Stubs.TeamANameStub);
            }

            // assert
            Assert.True(game.IsFinished);
        }
    }
}
