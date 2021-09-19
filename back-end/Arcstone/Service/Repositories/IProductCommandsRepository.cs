using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Repositories
{
    public interface IProductCommandsRepository
    {
        void SaveProduct(Products product);
    }
}
