using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };

            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            // Add Actor_movies
            foreach (var ActorId in data.ActorsIds)
            {
                Actor_Movie actor_movie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = ActorId
                };

                await _context.Actors_Movies.AddAsync(actor_movie);
            }

            await _context.SaveChangesAsync();
        }
        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == data.Id);

            if(dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
                await _context.SaveChangesAsync();
            }

            //Remove existing actors
            _context.Actors_Movies.RemoveRange(_context.Actors_Movies.Where(n => n.MovieId == dbMovie.Id).ToList());
            await _context.SaveChangesAsync();

            foreach (var actorId in data.ActorsIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };

                await _context.Actors_Movies.AddAsync(newActorMovie);
            }

            await _context.SaveChangesAsync();
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
