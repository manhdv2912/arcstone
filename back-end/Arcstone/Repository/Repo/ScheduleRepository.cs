using Contract.IRepo;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repo
{
    public class ScheduleRepository : RepositoryBase<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(ArcstoneContext arcstoneContext) : base(arcstoneContext)
        {

        }
    }
}
