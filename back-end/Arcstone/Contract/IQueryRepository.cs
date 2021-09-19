using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IQueryRepository<T> where T : class
    {
        Task<T> GetByID(int id);
        IQueryable<T> GetAll();

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    }
}
