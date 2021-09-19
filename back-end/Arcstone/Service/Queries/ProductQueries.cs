using Contract;
using Entity.Models;
using Repository;
using Service.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries
{
    public class ProductQueries : Query, IProductQueries
    {

        private readonly IQueryRepository<Products> _productQueriesRepository;
        public ProductQueries(IQueryRepository<Products> productQueriesRepository)
        {
            _productQueriesRepository = productQueriesRepository;
        }

        public async Task<Products> FindByID(int productId)
        {
            var prod = await _productQueriesRepository.GetByID(productId);
            return prod;
        }
        
    }
}
