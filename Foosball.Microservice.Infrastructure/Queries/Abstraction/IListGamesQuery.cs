using Foosball.Microservice.Infrastructure.Queries.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foosball.Microservice.Infrastructure.Queries.Abstraction
{
    public interface IListGamesQuery
    {
        Task<IReadOnlyCollection<GameListItem>> QueryAsync();
    }
}
