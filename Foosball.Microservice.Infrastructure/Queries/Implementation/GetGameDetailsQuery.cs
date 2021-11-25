using Dapper;
using Foosball.Microservice.DomainLogic.ValueObjects;
using Foosball.Microservice.Infrastructure.Queries.Abstraction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foosball.Microservice.Infrastructure.Queries.Implementation
{
    public class GetGameDetailsQuery : IGetGameDetailsQuery
    {
        private readonly IDbConnection _dbConnection;

        public GetGameDetailsQuery(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<string> QueryAsync(EntityId gameId)
        {
            using (_dbConnection)
            {
                var query = @"
                    SELECT
	                    game.TeamA AS TeamAName,
                        game.TeamB AS TeamBName,
                        game.IsFinished
                    FROM
	                    Game game
                    WHERE
                        game.Id = @gameId;

                    SELECT	
                        [set].[Order] AS SetOrder,
	                    1 AS IsTeamAGoal,
	                    GoalScoredAt = goal.ScoreAt
                    FROM
	                    Game game
                    INNER JOIN [Set] [set]
	                    ON [set].GameId = game.Id
                    INNER JOIN Goal goal
	                    ON goal.TeamASetId = [set].Id
                    WHERE
                        game.Id = @gameId
                    UNION ALL
                    SELECT	
                        [set].[Order] AS SetOrder,
	                    0 AS IsTeamAGoal,
	                    GoalScoredAt = goal.ScoreAt
                    FROM
	                    Game game
                    INNER JOIN [Set] [set]
	                    ON [set].GameId = game.Id
                    INNER JOIN Goal goal
	                    ON goal.TeamBSetId = [set].Id
                    WHERE
                        game.Id = @gameId
                ";

                var grid = await _dbConnection.QueryMultipleAsync(query, new { gameId = gameId.Id });
                var game = await grid.ReadSingleAsync<DtoGame>();
                var goals = await grid.ReadAsync<DtoGoal>();
                var scoreBoard = ConstructHumanReadableScoreBoard(game, goals);
                return scoreBoard;
            }
        }

        private static string ConstructHumanReadableScoreBoard(DtoGame game, IEnumerable<DtoGoal> goals)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Team A: {0} vs Team B: {1}", game.TeamAName, game.TeamBName);
            sb.AppendLine();

            var currentSet = null as int?;
            var teamASetGoals = null as int?;
            var teamBSetGoals = null as int?;
            var teamATotalGoals = 0;
            var teamBTotalGoals = 0;

            foreach (var goal in goals.OrderBy(x => x.SetOrder).ThenBy(x => x.GoalScoredAt))
            {
                // Next set
                if (goal.SetOrder != currentSet)
                {
                    currentSet = goal.SetOrder;
                    teamASetGoals = 0;
                    teamBSetGoals = 0;

                    sb.AppendLine();
                    sb.AppendFormat("Set {0} result:", currentSet.Value + 1);
                    sb.AppendLine();
                }

                string teamScored;

                if (goal.IsTeamAGoal)
                {
                    teamASetGoals++;
                    teamATotalGoals++;
                    teamScored = game.TeamAName;
                }
                else
                {
                    teamBSetGoals++;
                    teamBTotalGoals++;
                    teamScored = game.TeamBName;
                }

                sb.AppendFormat(
                    "Goal scored for {0} at {1:dd.MM.yyyy HH:mm:ss} - result: {2} {3}:{4} {5}", 
                    teamScored, 
                    goal.GoalScoredAt, 
                    game.TeamAName,
                    teamASetGoals,
                    teamBSetGoals,
                    game.TeamBName);
                sb.AppendLine();
            }

            if (game.IsFinished)
            {
                var winnerTeam = teamATotalGoals > teamBTotalGoals
                    ? game.TeamAName
                    : game.TeamBName;

                sb.AppendLine();
                sb.AppendFormat("Game is finished. The winner is {0}.", winnerTeam);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private class DtoGame
        {
            public string TeamAName { get; set; }

            public string TeamBName { get; set; }

            public bool IsFinished { get; set; }
        }

        private class DtoGoal
        {
            public int SetOrder { get; set; }

            public bool IsTeamAGoal { get; set; }

            public DateTime GoalScoredAt { get; set; }
        }
    }
}
