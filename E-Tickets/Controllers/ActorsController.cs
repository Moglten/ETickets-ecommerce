using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using E_Tickets.Models;
using Microsoft.AspNetCore.Authorization;
using E_Tickets.Models.ModelsDB;
using E_Tickets.Models.Operations;

namespace E_Tickets.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ActorsController : Controller
    {

        private readonly ActorOperations _actorOperations;
        private readonly IHostingEnvironment _appEnvironment;


        public ActorsController(IHostingEnvironment appEnvironment)
        {
            _actorOperations = new(appEnvironment);
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var data = _actorOperations.GetActors();
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public  IActionResult Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid) return View(actor);

            var files = HttpContext.Request.Form.Files;

            _actorOperations.CreateNewActor(files, actor);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var actorDetails = _actorOperations.GetSpacificActors(id);

            if (actorDetails == null) return View("NotFound");

            return View(actorDetails);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var actorDetails = _actorOperations.GetSpacificActors(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

       
        [HttpPost]
        public IActionResult Edit([FromForm]string oldphoto, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor newActor)
        {
            if (!ModelState.IsValid) return View(newActor);

           var files = HttpContext.Request.Form.Files;

            _actorOperations.UpdateActor(files, newActor, oldphoto);

            return RedirectToAction("Index");
        }

        //Get: Actors/Delete/1
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var actorDetails = _actorOperations.GetSpacificActors(id);

            if (actorDetails == null) return View("NotFound");

            return View(actorDetails);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        { 
            if (_actorOperations.DeleteActor(id)) return View("NotFound");

            return RedirectToAction("Index");
        }
    }
}

