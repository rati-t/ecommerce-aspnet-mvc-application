using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;
        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var cinemas = await _service.GetAllAsync();
            return View(cinemas);
        }


        //GET: Cinemas/Details
        public async Task<IActionResult> Details(int Id)
        {
            var producer = await _service.GetByIdAsync(Id);

            if (producer == null)
            {
                return View("NotFound");
            }
            return View(producer);
        }

        // GET: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo, Name, Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }


        //GET: Cinema/Edit

        public async Task<IActionResult> Edit(int Id)
        {
            var producer = await _service.GetByIdAsync(Id);
            if (producer == null)
            {
                return View("NotFound");
            }
            return View(producer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Logo, Name, Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }

            if (id == cinema.Id)
            {
                await _service.UpdateAsync(id, cinema);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // [HttpPost] HttpPost is not working here, search for explanation
        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _service.GetByIdAsync(id);

            if (cinema == null)
            {
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                return View("Item");
            }
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
