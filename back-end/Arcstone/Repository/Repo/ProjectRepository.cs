using Contract.IRepo;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repo
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(ArcstoneContext arcstoneContext) : base(arcstoneContext)
        {

        }
    }
}
