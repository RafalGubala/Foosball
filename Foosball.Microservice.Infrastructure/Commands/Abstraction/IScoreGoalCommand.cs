using Foosball.Microservice.DomainLogic.ValueObjects;
using System.Threading.Tasks;

namespace Foosball.Microservice.Infrastructure.Commands.Abstraction
{
    public interface IScoreGoalCommand
    {
        Task ExecuteAsync(EntityId gameId, TeamName team);
    }
}
