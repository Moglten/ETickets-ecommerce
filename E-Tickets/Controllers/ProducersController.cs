using E_Tickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using E_Tickets.Models.ModelsDB;
using E_Tickets.Models.Operations;

namespace E_Tickets.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ProducersController : Controller
    {
        private readonly ProducerOperations _producerOperations;
        private readonly IHostingEnvironment _appEnvironment;


        public ProducersController(IHostingEnvironment appEnvironment)
        {
            _producerOperations = new(appEnvironment);
            _appEnvironment = appEnvironment;

        }

        public IActionResult Index()
        {
            var allProducers = _producerOperations.GetProducers();

            return View(allProducers);
        }

        //GET: producers/create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);

            var PhotoFile = HttpContext.Request.Form.Files;

            _producerOperations.CreateNewProducer(PhotoFile, producer);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var producerDetails = _producerOperations.GetSpacificProducer(id);

            if (producerDetails == null) return View("NotFound");

            return View(producerDetails);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var producerDetails = _producerOperations.GetSpacificProducer(id);

            if (producerDetails == null) return View("NotFound");

            return View(producerDetails);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] string oldphoto,
                                  [Bind("Id,ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);

            var file = HttpContext.Request.Form.Files;

            _producerOperations.UpdateProducer(file, producer,oldphoto);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var producerDetails = _producerOperations.GetSpacificProducer(id);

            if (producerDetails == null) return View("NotFound");

            return View(producerDetails);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_producerOperations.DeleteProducer( id)) return View("NotFound");

            return RedirectToAction(nameof(Index));
        }
    }
}
