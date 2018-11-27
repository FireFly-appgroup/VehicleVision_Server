using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VehicleVision.DAL.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        List<TEntity> GetAll();
        List<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        bool Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        bool Delete(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        bool Edit(TEntity entity);
        bool Save();
        bool SaveChanges(TEntity entity);
        void Update(TEntity item);
        TEntity FindById(int id);
    }
}
