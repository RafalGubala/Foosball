using Foosball.Microservice.DomainLogic.AggregateRoot;
using Foosball.Microservice.DomainLogic.ValueObjects;
using Foosball.Microservice.Infrastructure.Commands.Abstraction;
using Foosball.Microservice.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Foosball.Microservice.Infrastructure.Commands.Implementation
{
    public class ScoreGoalCommand : IScoreGoalCommand
    {
        private readonly IRepository<Game> _gameRepository;

        public ScoreGoalCommand(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task ExecuteAsync(EntityId gameId, TeamName team)
        {
            var game = await _gameRepository.GetAsync(gameId);

            if (game == null)
            {
                throw new ArgumentException("No game for passed id", nameof(gameId));
            }

            game.ScoreGoal(team);
            await _gameRepository.UpdateAsync(game);
        }
    }
}
