using Contract.IRepo;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repo
{
    public class WeeklySummaryRepository : RepositoryBase<WeeklySummary>, IWeeklySummaryRepository
    {
        public WeeklySummaryRepository(ArcstoneContext arcstoneContext) : base(arcstoneContext)
        {

        }
    }
}
