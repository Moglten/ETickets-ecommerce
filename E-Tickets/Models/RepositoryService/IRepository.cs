using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Tickets.Models.RepositoryService
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAllEntities();
        IQueryable<T> GetAllEntitiesIQueryable();
        int GetNumberOfEntities();
        void Add(T entity);
        void Update(T entity);
        T GetEntity(int entityID);
        void Remove(int entityID);
        void RemoveRange(List<T> Entities);
        IEnumerable<T> GetResultBySqlProcedure(string query, params object[] paramters);

    }
}
