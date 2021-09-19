using Contract;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Bussiness
{
    public interface IProductService : IAppService
    {
        Task<Products> FindByID(int productId);
        Task CreateProduct(Products product);
    }
}
