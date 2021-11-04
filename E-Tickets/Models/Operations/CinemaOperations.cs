using E_Tickets.Models.ModelsDB;
using E_Tickets.Models.RepositoryService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Tickets.Models.Operations
{
    public class CinemaOperations
    {
        private readonly UnitOfWork _unitOfWork = new();
        private readonly IRepository<Cinema> _cinema;
        private readonly IHostingEnvironment _appEnvironment;


        public CinemaOperations(IHostingEnvironment appEnvironment)
        {
            _cinema = _unitOfWork.GetRepositoryInstance<Cinema>();
            _appEnvironment = appEnvironment;

        }

        public Cinema GetSpacificCinemas(int id)
        {
            return _cinema.GetEntity(id);
        }

        public IEnumerable<Cinema> GetCinemas()
        {
            return _cinema.GetAllEntities();
        }


        public void CreateNewCinema(IFormFileCollection photoFile, Cinema Cinema)
        {
            UploadPhoto.UploadCinemaPhoto(_appEnvironment, photoFile, Cinema, null);

            _cinema.Add(Cinema);
        }



        public void UpdateCinema(IFormFileCollection photoFile, Cinema cinema, string oldphoto)
        {
            UploadPhoto.UploadCinemaPhoto(_appEnvironment, photoFile, cinema, oldphoto);

            _cinema.Update(cinema);
        }


        public bool DeleteCinema(int id)
        {
            try
            {
                _cinema.Remove(id);

                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
