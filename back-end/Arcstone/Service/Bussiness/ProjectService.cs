using Entity.Models;
using Repository;
using Service.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Bussiness
{
    public class ProjectService : AppService, IProjectService
    {
        private readonly IProjectQueries _projectQueries;
        public ProjectService(IProjectQueries projectQueries)
        {
            _projectQueries = projectQueries;
        }
        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _projectQueries.GetAll();
        }
    }
}
