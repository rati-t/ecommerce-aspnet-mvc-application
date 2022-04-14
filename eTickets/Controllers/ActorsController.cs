using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorService _service;
        public ActorsController(IActorService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var actors = await _service.GetAllAsync();
            return View(actors);
        }

        // GET: Actor/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio")]Actor actor)
        {
            if(!ModelState.IsValid){

                return View();
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        //GET: Actor/Details
        public async Task<IActionResult> Details(int Id)
        {
            var actor = await _service.GetByIdAsync(Id);

            if (actor == null) {
                return View("NotFound");
            }
            return View(actor);
        }

        //GET: Actor/Edit

        public async Task<IActionResult> Edit(int Id)
        {
            var actor = await _service.GetByIdAsync(Id);
            if (actor == null) 
            {
                return View("NotFound");
            } 
            return View(actor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePictureURL, Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }

        // [HttpPost] HttpPost is not working here, search for explanation
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return View("Item");
            }
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
