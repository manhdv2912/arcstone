using Contract;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries
{
    public interface IProjectQueries
        : IQuery
    {
        Task<IEnumerable<Project>> GetAll();
    }
}
