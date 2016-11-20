using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> List(Expression<Func<T, bool>> filter = null);

        T Find(int id);

        void Add(T postToAdd);

        void Edit(int id, T postToUpdate);

        void Delete(int id);
    }
}
