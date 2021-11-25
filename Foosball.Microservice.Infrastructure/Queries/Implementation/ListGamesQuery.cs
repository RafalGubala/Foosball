using Foosball.Microservice.Infrastructure.Queries.Abstraction;
using Foosball.Microservice.Infrastructure.Queries.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Foosball.Microservice.Infrastructure.Queries.Implementation
{
    public class ListGamesQuery : IListGamesQuery
    {
        private readonly IDbConnection _dbConnection;

        public ListGamesQuery(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IReadOnlyCollection<GameListItem>> QueryAsync()
        {
            using (_dbConnection)
            {
                const string query = @"
                    SELECT
                        Id,
                        TeamA AS TeamAName,
                        TeamB AS TeamBName
                    FROM
                        Game
                    ORDER BY 
                        CreatedAt DESC";

                var games = await _dbConnection.QueryAsync<GameListItem>(query);
                return games.ToList();
            }
        }
    }
}
