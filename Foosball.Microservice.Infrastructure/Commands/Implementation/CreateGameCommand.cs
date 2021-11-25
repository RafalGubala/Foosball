using Foosball.Microservice.DomainLogic.AggregateRoot;
using Foosball.Microservice.DomainLogic.ValueObjects;
using Foosball.Microservice.Infrastructure.Commands.Abstraction;
using Foosball.Microservice.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Foosball.Microservice.Infrastructure.Commands.Implementation
{
    public class CreateGameCommand : ICreateGameCommand
    {
        private readonly IRepository<Game> _gameRepository;

        public CreateGameCommand(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task ExecuteAsync(EntityId id, string teamAName, string teamBName)
        {
            var game = Game.Create(
                id,
                TeamName.Create(teamAName),
                TeamName.Create(teamBName));
            await _gameRepository.CreateAsync(game);
        }
    }
}
