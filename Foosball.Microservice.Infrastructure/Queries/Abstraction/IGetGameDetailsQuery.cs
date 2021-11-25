using Foosball.Microservice.DomainLogic.ValueObjects;
using System.Threading.Tasks;

namespace Foosball.Microservice.Infrastructure.Queries.Abstraction
{
    public interface IGetGameDetailsQuery
    {
        Task<string> QueryAsync(EntityId gameId);
    }
}
