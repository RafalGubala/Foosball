using System.Threading.Tasks;
using Foosball.Microservice.DomainLogic.ValueObjects;

namespace Foosball.Microservice.Infrastructure.Commands.Abstraction
{
    public interface ICreateGameCommand
    {
        Task ExecuteAsync(EntityId id, string teamAName, string teamBName);
    }
}
