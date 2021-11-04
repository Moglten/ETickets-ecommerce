using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using E_Tickets.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using E_Tickets.Models.ViewModels;
using E_Tickets.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using E_Tickets.Models.Operations;

namespace E_Tickets.Controllers
{
    public class MoviesController : Controller
    {

        private readonly MovieOperations _movieOperation;
        private readonly ActorOperations _actorOperation;
        private readonly ProducerOperations _producerOperation;
        private readonly CinemaOperations _cinemaOperations;

        public MoviesController(IHostingEnvironment appEnvironment)
        {
            _movieOperation = new(appEnvironment);
            _actorOperation = new(appEnvironment);
            _producerOperation = new(appEnvironment);
            _cinemaOperations = new(appEnvironment);
        }


        [HttpGet]
        public IActionResult Index()
        {
            var allMovies = _movieOperation.GetMovies();

            return View(allMovies);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var movieDetail = _movieOperation.GetMovieDetailed(id);

            return View(movieDetail);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var dropdownlistdate = GetDropDownListData();
            ViewBag.Actors = dropdownlistdate.actors;
            ViewBag.Producers = dropdownlistdate.producers;
            ViewBag.Cinema = dropdownlistdate.cinemas;

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(MovieViewModel movieViewModel)
        {
            if (!ModelState.IsValid) return View(movieViewModel);

            _movieOperation.CreateNewMovie(movieViewModel);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        { 
            var movieDetails = _movieOperation.GetSpacificMovie(id);

            if (movieDetails == null) return View("NotFound");

            var movie = _movieOperation.MappingToViewModel(movieDetails);

            var dropdownlistdate = GetDropDownListData();
            ViewBag.Actors = dropdownlistdate.actors;
            ViewBag.Producers = dropdownlistdate.producers;
            ViewBag.Cinema = dropdownlistdate.cinemas;

            return View(movie);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(int id, MovieViewModel movie)
        {
            if (id != movie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var dropdownlistdate = GetDropDownListData();
                ViewBag.Actors = dropdownlistdate.actors;
                ViewBag.Producers = dropdownlistdate.producers;
                ViewBag.Cinema = dropdownlistdate.cinemas;

                return View(movie);
            }

            _movieOperation.UpdateMovie(movie);
            return View("Index", _movieOperation.GetMovies());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteConfirmed(int id)
        {

            if (_movieOperation.DeleteMovie(id)) return View("NotFound");

            return View("Index", _movieOperation.GetMovies());
        }


        [HttpPost]
        public IActionResult Filter(string searchString)
        {
            var movies = _movieOperation.FilterMovie(searchString);

            ViewBag.searchString = searchString;

            return View("Index", movies);
        }


        private DropDownListData GetDropDownListData()
        {
            return new DropDownListData()
            {
                actors = new MultiSelectList(_actorOperation.GetActors().OrderBy(n => n.FullName).ToList(), "Id", "FullName"),
                producers = new MultiSelectList(_producerOperation.GetProducers().OrderBy(n => n.FullName).ToList(), "Id", "FullName"),
                cinemas = new SelectList(_cinemaOperations.GetCinemas().OrderBy(n => n.Name).ToList(), "Id", "Name")
               
            };

        }

    }

}

