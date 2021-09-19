using Contract;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Bussiness
{
    public interface IProjectService : IAppService
    {
        Task<IEnumerable<Project>> GetAll();
    }
}
