using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries
{
    public class WeeklySummaryQueries : Query, IWeeklySummaryQueries
    {
        private readonly IQueryRepository<WeeklySummary> _weeklySummaryQueriesRepository;

        public WeeklySummaryQueries(IQueryRepository<WeeklySummary> weeklySummaryQueriesRepository)
        {
            _weeklySummaryQueriesRepository = weeklySummaryQueriesRepository;
        }
        public async Task<WeeklySummary> FindWeekInYear(int weekInYear, int year)
        {
            var weekly = await _weeklySummaryQueriesRepository.FindByCondition(w => w.WeekIndex == weekInYear && w.Year == year).FirstOrDefaultAsync();
            return weekly;
        }
    }
}
