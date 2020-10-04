using SPATestTask.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPATestTask.Core.Repositories;
using SPATestTask.DAL.Data;

namespace SPATestTask.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        public Repository(DbContextSPATestTask contextSpaTestTask)
        {
            this.contextSpaTestTask = contextSpaTestTask;
            dbSet = contextSpaTestTask.Set<T>();
        }
        protected DbContextSPATestTask contextSpaTestTask;
        protected DbSet<T> dbSet { get; set; }

        protected IQueryable<T> DbSetInclude { get; set; }


        public async Task<T> AddAsync(T entity)
        {
            var entry = await dbSet.AddAsync(entity);

            return entry.Entity;
        }
        protected IQueryable<T> GetInclude()
        {
            return DbSetInclude != null ? DbSetInclude : dbSet;
        }

        public virtual List<T> GetAll()
        {
            return GetInclude().ToList();
        }

        public virtual List<T> GetAllOfPage(int pageNumber, int rowCount)
        {
            int startIndex = (pageNumber - 1) * rowCount;
            return GetInclude()
                   .Skip(startIndex)
                   .Take(rowCount)
                   .ToList();
        }
        //identity
        public async Task<List<T>> GetAllAsync()
        {
            return await GetInclude().ToListAsync();
        }
        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteALot(List<T> entityList)
        {
            for (int i = 0; i < entityList.Count; i++)
            {
                dbSet.Remove(entityList[i]);
            }
        }

        public void Save()
        {
            contextSpaTestTask.SaveChanges();
        }
    }

    public class Repository<T, TId> : Repository<T>, IRepository<T, TId>
        where T : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        public Repository(DbContextSPATestTask contextSpaTestTask)
            : base(contextSpaTestTask)
        {
        }

        public async Task<List<T>> GetAllOfIdAsync(List<TId> idList)
        {
            return await GetInclude().Where(x => idList.Any(n => x.Id.Equals(n))).ToListAsync();
        }

        public virtual T GetById(TId id)
        {
            var retVal = GetInclude().FirstOrDefault(x => x.Id.Equals(id));
            if (retVal == null) throw  new  NullReferenceException("Не найдеена запись");
            return retVal;
        }
    }
}
