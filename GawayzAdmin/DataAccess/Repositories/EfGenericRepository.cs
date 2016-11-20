using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        readonly DbContext _db;
        readonly IDbSet<T> _entity;
        public EfGenericRepository(DbContext db)
        {
            _db = db;
            _entity = _db.Set<T>();
        }
        //Lambda expression
        public IQueryable<T> List(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return _entity.Where(filter);
            }
            return _entity;
        }

        public T Find(int id)
        {
            return _entity.Find(id);
        }

        public void Add(T entityToAdd)
        {
            _entity.Add(entityToAdd);
        }

        public void Edit(int id, T entitToUpdate)
        {
            _entity.Attach(entitToUpdate);
            _db.Entry(entitToUpdate).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var selectedEntity = _entity.Find(id);
            _db.Entry(selectedEntity).State = EntityState.Deleted;
        }
    }
}
