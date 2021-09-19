using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{
    public class ProductQueriesRepository : QueryRepository<Products>, IProductQueriesRepository
    {
        public ProductQueriesRepository(IRepositoryWrapper repoWrapper) : base(repoWrapper)
        {
        }
    }
}
