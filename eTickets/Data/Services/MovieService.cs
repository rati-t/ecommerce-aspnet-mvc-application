using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class MovieService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MovieService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Movie> GetMovieByIdAsync(int Id)
        {
            var movie = await _context.Movies
                        .Include(m => m.Cinema)
                        .Include(m => m.Producer)
                        .Include(m => m.Actors_Movies).ThenInclude(am => am.Actor)
                        .FirstOrDefaultAsync(m => m.Id == Id);
            return movie;
        }

        public async Task<NewMovieDropdownVm> GetNewMovieDropdownListAsycn()
        {
            NewMovieDropdownVm newMovieDropdownList = new NewMovieDropdownVm()
            {
                Cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync(),
                Actors = await _context.Actors.OrderBy(c => c.FullName).ToListAsync(),
                Produers = await _context.Producers.OrderBy(c => c.FullName).ToListAsync()
            };

            return newMovieDropdownList;
        }
    }
}
