using Foosball.Microservice.Api.Models;
using Foosball.Microservice.DomainLogic.ValueObjects;
using Foosball.Microservice.Infrastructure.Commands.Abstraction;
using Foosball.Microservice.Infrastructure.Queries.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Foosball.Microservice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ICreateGameCommand _createGameCommand;
        private readonly IScoreGoalCommand _scoreGoalCommand;
        private readonly IGetGameDetailsQuery _getGameDetailsQuery;
        private readonly IListGamesQuery _listGamesQuery;

        public GameController(
            ICreateGameCommand createGameCommand,
            IScoreGoalCommand scoreGoalCommand,
            IGetGameDetailsQuery getGameDetailsQuery,
            IListGamesQuery listGamesQuery)
        {
            _createGameCommand = createGameCommand;
            _scoreGoalCommand = scoreGoalCommand;
            _getGameDetailsQuery = getGameDetailsQuery;
            _listGamesQuery = listGamesQuery;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGamePostModel model)
        {
            var newGameId = EntityId.New();
            var teamAName = TeamName.Create(model.TeamA);
            var teamBName = TeamName.Create(model.TeamB);
            await _createGameCommand.ExecuteAsync(newGameId, teamAName, teamBName);
            return Ok(new { id = newGameId.Id });
        }

        [HttpPut("{id}/score/{team}")]
        public async Task<IActionResult> ScoreGoalAsync(Guid id, string team)
        {
            var gameId = EntityId.From(id);
            var teamName = TeamName.Create(team);
            await _scoreGoalCommand.ExecuteAsync(gameId, teamName);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailsAsync(Guid id)
        {
            var humanReadableScoreboard = await _getGameDetailsQuery.QueryAsync(EntityId.From(id));
            return Ok(humanReadableScoreboard);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var games = await _listGamesQuery.QueryAsync();
            return Ok(games);
        }
    }
}
