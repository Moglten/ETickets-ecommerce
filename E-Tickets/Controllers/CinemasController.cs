using E_Tickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using E_Tickets.Models.ModelsDB;
using E_Tickets.Models.Operations;

namespace E_Tickets.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CinemasController : Controller
    {
        private readonly CinemaOperations _cinemaOperations;
        private readonly IHostingEnvironment _appEnvironment;


        public CinemasController(IHostingEnvironment appEnvironment)
        {
            _cinemaOperations = new(appEnvironment);
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            var allCinemas = _cinemaOperations.GetCinemas();

            return View(allCinemas);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);

            var files = HttpContext.Request.Form.Files;

            _cinemaOperations.CreateNewCinema(files,cinema);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var cinemaDetails = _cinemaOperations.GetSpacificCinemas(id);

            if (cinemaDetails == null) return View("NotFound");

            return View(cinemaDetails);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cinemaDetails = _cinemaOperations.GetSpacificCinemas(id);

            if (cinemaDetails == null) return View("NotFound");

            return View(cinemaDetails);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] string oldphoto, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);

            var files = HttpContext.Request.Form.Files;

            _cinemaOperations.UpdateCinema(files,cinema,oldphoto);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var cinemaDetails = _cinemaOperations.GetSpacificCinemas(id);

            if (cinemaDetails == null) return View("NotFound");

            return View(cinemaDetails);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            if (_cinemaOperations.DeleteCinema(id)) return View("NotFound");

            return RedirectToAction(nameof(Index));
        }
    }
}