using eTickets.Data;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _service.GetAllAsync((n => n.Cinema));
            return View(movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            return View(movie);
        }

        // GET: Movie/Create
        public async Task<IActionResult> Create()
        {
            var movieDropdownData = await _service.GetNewMovieDropdownListAsycn();

            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(movieDropdownData.Produers, "Id", "FullName");


            return View();
        }

    }
}
