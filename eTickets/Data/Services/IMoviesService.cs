using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        public Task<Movie> GetMovieByIdAsync(int Id);
        public Task<NewMovieDropdownVm> GetNewMovieDropdownListAsycn();
        public Task AddNewMovieAsync(NewMovieVM data);
        public Task UpdateMovieAsync(NewMovieVM data);

    }
}
