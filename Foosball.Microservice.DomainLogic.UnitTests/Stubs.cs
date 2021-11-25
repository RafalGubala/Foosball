using Foosball.Microservice.DomainLogic.AggregateRoot;
using Foosball.Microservice.DomainLogic.ValueObjects;
using System;
using Foosball.Microservice.DomainLogic.Entities;

namespace Foosball.Microservice.DomainLogic.UnitTests
{
    public static class Stubs
    {
        public static readonly EntityId GameIdStub = EntityId.From(Guid.Parse("f5061c86-60f7-4703-bded-81681be7f743"));
        public static readonly TeamName TeamANameStub = TeamName.Create("Team A");
        public static readonly TeamName TeamBNameStub = TeamName.Create("Team B");

        public static Set GetNewSet() => Set.Create(SetOrder.CreateFrom(0));

        public static Game GetNewGame() => Game.Create(GameIdStub, TeamANameStub, TeamBNameStub);

        public static Game GetFinishedGame()
        {
            var game = Game.Create(GameIdStub, TeamANameStub, TeamBNameStub);

            for (var i = 0; i < 20; i++)
            {
                game.ScoreGoal(TeamANameStub);
            }

            return game;
        }
    }
}
