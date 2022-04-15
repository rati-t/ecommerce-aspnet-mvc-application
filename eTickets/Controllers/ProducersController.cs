using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducerService _service;
        public ProducersController(IProducerService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var producers = await _service.GetAllAsync();
            return View(producers);
        }

        // GET: Prodocurs/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }
            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        //GET: Producers/Details
        public async Task<IActionResult> Details(int Id)
        {
            var producer = await _service.GetByIdAsync(Id);

            if (producer == null)
            {
                return View("NotFound");
            }
            return View(producer);
        }

        //GET: Actor/Edit

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
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePictureURL, Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }

            if(id == producer.Id)
            {
                await _service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // [HttpPost] HttpPost is not working here, search for explanation
        public async Task<IActionResult> Delete(int id)
        {
            var producer = await _service.GetByIdAsync(id);

            if (producer == null)
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
