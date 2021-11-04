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
    public class ProducerOperations
    {
        private readonly UnitOfWork _unitOfWork = new();
        private readonly IRepository<Producer> _producers;
        private readonly IHostingEnvironment _appEnvironment;


        public ProducerOperations(IHostingEnvironment appEnvironment)
        {
            _producers = _unitOfWork.GetRepositoryInstance<Producer>();
            _appEnvironment = appEnvironment;       

        }

        public Producer GetSpacificProducer(int id)
        {
            return _producers.GetEntity(id);
        }

        public IEnumerable<Producer> GetProducers()
        {
            return _producers.GetAllEntities();
        }


        public void CreateNewProducer(IFormFileCollection  photoFile,Producer producer)
        {
            UploadPhoto.UploadProducerPhoto(_appEnvironment, photoFile, producer,null);

            _producers.Add(producer);
        }

        

        public void UpdateProducer(IFormFileCollection photoFile, Producer producer, string oldphoto)
        {
            UploadPhoto.UploadProducerPhoto(_appEnvironment, photoFile, producer, oldphoto);

            _producers.Update(producer);
        }


        public bool DeleteProducer(int id)
        {
            try
            {
                _producers.Remove(id);
                return true;
            }
            catch {
                return false;
            }

        }


    }
}
