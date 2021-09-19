using Contract;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries
{
    public interface IWeeklySummaryQueries : IQuery
    {
        Task<WeeklySummary> FindWeekInYear(int weekInYear, int year);
    }
}
