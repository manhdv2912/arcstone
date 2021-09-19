using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class QueryRepository<T> : IQueryRepository<T> where T : BaseEntity
    {

        private readonly IRepositoryWrapper _repoWrapper;
        public QueryRepository(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public IQueryable<T> GetAll()
        {
            return _repoWrapper.Resolve<T>().FindAll();
        }

        public async Task<T> GetByID(int id)
        {
            return await _repoWrapper.Resolve<T>().FindByCondition(w => w.Id == id).FirstOrDefaultAsync();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _repoWrapper.Resolve<T>().FindByCondition(expression);
        }
    }
}
