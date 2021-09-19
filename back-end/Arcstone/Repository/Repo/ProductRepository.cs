using Contract;
using Contract.IRepo;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repo
{
    public class ProductRepository : RepositoryBase<Products>, IProductRepository
    {
        public ProductRepository(ArcstoneContext arcstoneContext) : base(arcstoneContext)
        {

        }
    }
}
