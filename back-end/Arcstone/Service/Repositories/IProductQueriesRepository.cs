using Contract;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{
    public interface IProductQueriesRepository : IQueryRepository<Products>
    {
    }
}
