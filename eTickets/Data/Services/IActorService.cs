using eTickets.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IActorService
    {
        public Task<IEnumerable<Actor>> GetAllAsync();

        public Task<Actor> GetByIdAsync(int Id);

        public Task<Actor> UpdateAsync(int id, Actor newActor);

        public Task DeleteAsync(int id);

        public Task AddAsync(Actor actor);
    }
}
