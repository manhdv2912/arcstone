using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries
{
    public class ProjectQueries :
        Query,
        IProjectQueries
    {
        private readonly IQueryRepository<Project> _projectQueriesRepository;
        public ProjectQueries(IQueryRepository<Project> projectQueriesRepository)
        {
            _projectQueriesRepository = projectQueriesRepository;
        }
        public async Task<IEnumerable<Project>> GetAll()
        {
            var rs = await _projectQueriesRepository.GetAll().ToListAsync();
            return rs;
        }
    }
}
