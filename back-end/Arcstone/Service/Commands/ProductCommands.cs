using Contract;
using Entity.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Commands
{
    public class ProductCommands : Command, IProductCommands
    {
        private readonly ICommandRepository<Products> _productCommandRepository;
        public ProductCommands(ICommandRepository<Products> productCommandRepository)
        {
            _productCommandRepository = productCommandRepository;
        }
        public async Task CreateProduct(Products entity)
        {
            await _productCommandRepository.Create(entity);
            await Task.CompletedTask;
        }
    }
}
