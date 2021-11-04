using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Tickets.Models;
using E_Tickets.Data;
using Microsoft.EntityFrameworkCore;
using E_Tickets.Models.ModelsDB;

namespace E_Tickets.Models.RepositoryService
{
    public class UnitOfWork
    {
        private readonly eTicketDbContext DbEntity = new();

        public IRepository<T> GetRepositoryInstance<T>() where T : class
        {
            return new Repository<T>(DbEntity);
        }

        public eTicketDbContext GetDBInstance()
        {
            return DbEntity;
        }

        public void SaveChanges()
        {
            DbEntity.SaveChanges();
        }

    }
}
