using System;
using Foosball.Microservice.DomainLogic.AggregateRoot;
using Foosball.Microservice.DomainLogic.ValueObjects;
using Foosball.Microservice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Foosball.Microservice.Infrastructure.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private readonly FoosballDbContext _dbContext;

        public GameRepository(FoosballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Game entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _dbContext.Game.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Game> GetAsync(EntityId id)
        {
            var game = await _dbContext.Game.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return game;
        }

        public async Task UpdateAsync(Game entity)
        {
            _dbContext.Game.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
