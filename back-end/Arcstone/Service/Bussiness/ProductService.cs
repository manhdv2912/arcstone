using Entity.Models;
using Repository;
using Service.Commands;
using Service.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Bussiness
{
    public class ProductService : AppService, IProductService
    {
        private readonly IProductQueries _productQueries;
        private readonly IProductCommands _productCommands;
        public ProductService(IProductQueries productQueries,
                                IProductCommands productCommands)
        {
            _productQueries = productQueries;
            _productCommands = productCommands;
        }
        public Task<Products> FindByID(int productId)
        {
            return _productQueries.FindByID(productId);
        }

        public async Task CreateProduct(Products product)
        {
            await _productCommands.CreateProduct(product);
            
            await Task.CompletedTask;
        }
    }
}
