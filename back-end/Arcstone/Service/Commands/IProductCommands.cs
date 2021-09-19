using Contract;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Commands
{
    public interface IProductCommands : ICommand
    {
        Task CreateProduct(Products entity);
    }
}
