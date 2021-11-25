using Foosball.Microservice.DomainLogic.AggregateRoot;
using Foosball.Microservice.DomainLogic.ValueObjects;
using System.Threading.Tasks;

namespace Foosball.Microservice.Infrastructure.Repositories
{
    public interface IRepository<T>
        where T : IAggregateRoot
    {
        Task CreateAsync(T entity);

        Task<T> GetAsync(EntityId id);

        Task UpdateAsync(T entity);
    }
}
