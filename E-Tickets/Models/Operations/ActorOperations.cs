using E_Tickets.Models.ModelsDB;
using E_Tickets.Models.Operations;
using E_Tickets.Models.RepositoryService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;


namespace E_Tickets.Models.Operations
{
    public class ActorOperations
    {

        private readonly UnitOfWork _unitOfWork = new();
        private readonly IRepository<Actor> _actors;
        private readonly IHostingEnvironment _appEnvironment;


        public ActorOperations(IHostingEnvironment appEnvironment)
        {
            _actors = _unitOfWork.GetRepositoryInstance<Actor>();
            _appEnvironment = appEnvironment;

        }

        public Actor GetSpacificActors(int id)
        {
            return _actors.GetEntity(id);
        }

        public IEnumerable<Actor> GetActors()
        {
            return _actors.GetAllEntities();
        }


        public void CreateNewActor(IFormFileCollection photoFile, Actor actor)
        {
            UploadPhoto.UploadActorPhoto(_appEnvironment, photoFile, actor, null);

            _actors.Add(actor);
        }



        public void UpdateActor(IFormFileCollection photoFile, Actor actor, string oldphoto)
        {
            UploadPhoto.UploadActorPhoto(_appEnvironment, photoFile, actor, oldphoto);

            _actors.Update(actor);
        }


        public bool DeleteActor(int id)
        {
            try
            {
                _actors.Remove(id);
                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
