using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using E_Tickets.Data;
using E_Tickets.Models;
using Microsoft.EntityFrameworkCore;
using E_Tickets.Models.ModelsDB;

namespace E_Tickets.Models.RepositoryService
{
    public class Repository<T> : IRepository<T> where T:class
    {
        private readonly eTicketDbContext _context;
        private readonly DbSet<T> _dbset;

        public Repository(eTicketDbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAllEntities()
        {
            return _dbset.ToList();
        }


        public IQueryable<T> GetAllEntitiesIQueryable()
        {
            return _dbset;
        }

        public T GetEntity(int entityID)
        {
            return _dbset.Find(entityID);
        }

        public int GetNumberOfEntities()
        {
            return _dbset.Count();
        }

        public IEnumerable<T> GetResultBySqlProcedure(string query, params object[] paramters)
        {
            if(paramters != null)
            {
                return _dbset.FromSqlRaw<T>(query, paramters).ToList();
            }
            else
            {
                return _dbset.FromSqlRaw<T>(query).ToList();
            }
            
        }

        public void Remove(int entityID)
        {
            _dbset.Remove(GetEntity(entityID));
            _context.SaveChanges();
        }
        public void RemoveRange(List<T> Entities)
        {
            _dbset.RemoveRange(Entities);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
            _context.SaveChanges();
        }

        
    }
}
