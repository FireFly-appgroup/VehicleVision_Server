using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleVision.DAL.Models;

namespace VehicleVision.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private VehicleVisionDBContext _entities;

        public GenericRepository(VehicleVisionDBContext context)
        {
            _entities = context;
        }

        public VehicleVisionDBContext database
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public virtual List<T> GetAll()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query.ToList();
        }

        public List<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query.ToList();
        }
        public virtual void Attach(T entity)
        {
            _entities.Set<T>().Attach(entity);
        }

        public virtual bool Add(T entity)
        {
            _entities.Set<T>().Add(entity);
            return true;
        }

        public virtual bool Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
            return true;
        }

        public virtual bool Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public virtual bool Save()
        {
            _entities.SaveChanges();
            return true;
        }

        public virtual bool SaveChanges(T entity)
        {
            if (_entities.Entry(entity).State == EntityState.Detached)
            {
                _entities.Set<T>().Attach(entity);
            }
            _entities.Entry(entity).State = EntityState.Modified;
            _entities.SaveChanges();
            return true;
        }

        public virtual T FindById(int id)
        {
            return _entities.Set<T>().Find(id);
        }

        public T Get(int id)
        {
            return _entities.Set<T>().Find(id);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.Set<T>().AddRange(entities);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.Set<T>().RemoveRange(entities);
        }

        public void Update(T item)
        {
            _entities.Entry(item).State = EntityState.Modified;
        }
    }
}