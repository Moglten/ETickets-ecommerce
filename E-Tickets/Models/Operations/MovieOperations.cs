using E_Tickets.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_Tickets.Models;
using Microsoft.AspNetCore.Hosting;
using System.Web.Mvc;
using E_Tickets.Models.ModelsDB;
using E_Tickets.Models.RepositoryService;
using System.IO;

namespace E_Tickets.Models.Operations
{
    public class MovieOperations
    {
        private readonly UnitOfWork _unitOfWork = new();
        private readonly IRepository<Movie> _movies;
        private readonly IRepository<ActorsMovie> _moviesActors;
        private readonly IRepository<MoviesCategory> _movieCategory;
        private readonly IHostingEnvironment _appEnvironment;

        public MovieOperations(IHostingEnvironment appEnvironment)
        {
            _movies = _unitOfWork.GetRepositoryInstance<Movie>();
            _moviesActors = _unitOfWork.GetRepositoryInstance<ActorsMovie>();
            _movieCategory = _unitOfWork.GetRepositoryInstance<MoviesCategory>();

            _appEnvironment = appEnvironment;
        }



        public IEnumerable<Movie> GetMovies()
        {
            return _movies.GetAllEntitiesIQueryable()
                          .Include(c => c.Cinema)
                          .OrderBy(m => m.Name)
                          .ToList();
        }
            

        public Movie GetMovieDetailed(int id)
        {
            return _movies.GetAllEntitiesIQueryable()
                          .Include(m => m.Cinema)
                          .Include(m => m.Producer)
                          .Include(m => m.MoviesCategories).ThenInclude(m => m.Category)
                          .Include(m => m.ActorsMovies).ThenInclude(m=>m.Actor)
                          .FirstOrDefault(m => m.Id == id);
        }

        public Movie GetSpacificMovie(int id)
        {
            return _movies.GetEntity(id);
        }


        public Movie UpdateMovie(MovieViewModel movieViewModel)
        {
            var movie = MappingToMovie(movieViewModel,true);

            _movies.Update(movie);

            return movie;
        }


        public void CreateNewMovie(MovieViewModel movieViewModel)
        {
            var movie = MappingToMovie(movieViewModel,false);

            _movies.Add(movie);

            var id = _movies.GetAllEntitiesIQueryable().FirstOrDefault(a => a.Name == movie.Name).Id;

            // Append to the Categories table and Actors table 
            movieViewModel.Categories
                          .ForEach(m => _movieCategory
                          .Add(new MoviesCategory()
                          {
                              CategoryId = m,
                              MovieId = id
                          }));

            movieViewModel.ActorId
                          .ForEach(m => _moviesActors
                          .Add(new ActorsMovie()
                          {
                              ActorId = m,
                              MovieId = id
                          }));
        }


        public bool DeleteMovie(int id)
        {
            var movie = _movies.GetEntity(id);
            try
            {
                _movies.Remove(id);
                UploadPhoto.DeleteOldPhoto(Path.Combine(_appEnvironment.WebRootPath, "media"), movie.ImageUrl);
                return true;
            }
            catch {
                return false;
            }
        }


        public IEnumerable<Movie> FilterMovie(string SearchString)
        {
           return _movies.GetAllEntitiesIQueryable()
                                .Where(m => m.Name.ToLower().Contains(SearchString.ToLower()) 
                                    || m.Description.ToLower().Contains(SearchString.ToLower()))
                                .Include(c => c.Cinema)
                                .OrderBy(m => m.Name)
                                .ToList();
        }


            


        public Movie MappingToMovie(MovieViewModel movieViewModel, bool Editing)
        {
            if(!(Editing))
            {
                //Creating new photo or deal with edit without changing the photo
                var movie = new Movie()
                {
                    Name = movieViewModel.Name,
                    Description = movieViewModel.Description,
                    ImageUrl = UploadPhoto.UploadMoviePhoto(_appEnvironment,
                                                            movieViewModel,
                                                            null),
                    StartDate = movieViewModel.StartDate,
                    EndDate = movieViewModel.EndDate,
                    ProducerId = movieViewModel.ProducerId,
                    CinemaId = movieViewModel.CinemaId,
                    Price = movieViewModel.Price,
                    
                };
                return movie;
            }
            else
            {
                // Editing of the movie and photo
                var movie = new Movie()
                {
                    Id = movieViewModel.Id,
                    Name = movieViewModel.Name,
                    Description = movieViewModel.Description,
                    ImageUrl = UploadPhoto.UploadMoviePhoto(_appEnvironment,
                                                            movieViewModel,
                                                            _movies.GetEntity(movieViewModel.Id).ImageUrl),
                    StartDate = movieViewModel.StartDate,
                    EndDate = movieViewModel.EndDate,
                    ProducerId = movieViewModel.ProducerId,
                    CinemaId = movieViewModel.CinemaId,
                    Price = movieViewModel.Price
                };
                return movie;
            }
            
        }


        public MovieViewModel MappingToViewModel(Movie movieModel)
        {
            return new MovieViewModel()
            {
                Id = movieModel.Id,
                Name = movieModel.Name,
                Description = movieModel.Description,
                Price = movieModel.Price,
                StartDate = movieModel.StartDate,
                EndDate = movieModel.EndDate,
                ImageName = movieModel.ImageUrl,
                Categories = movieModel.MoviesCategories.Select(b=> b.CategoryId).ToList(),
                CinemaId = movieModel.CinemaId,
                ProducerId = movieModel.ProducerId,
                ActorId = movieModel.ActorsMovies.Select(b => b.ActorId).ToList(),
            };  
        }
    }
}
