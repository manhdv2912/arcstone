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
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        public ArcstoneContext ArcstoneContext { get; set; }
        public RepositoryBase(ArcstoneContext arcstoneContext)
        {
            ArcstoneContext = arcstoneContext;
        }
        public void Create(T entity)
        {
            ArcstoneContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            ArcstoneContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return ArcstoneContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return ArcstoneContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            ArcstoneContext.Set<T>().Update(entity);
        }
    }
}
