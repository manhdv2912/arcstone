using Contract;
using Entity.Models;
using Service.DTOs.ScheduleDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Bussiness
{
    public interface IWeeklySummaryService : IAppService
    {
        Task<WeeklySummaryDto> FindCurrentWeek();

        Task<WeeklySummary> FindWeekIndex(int weekIndex, int year);
        Task Create(WeeklySummary input);
        Task Update(WeeklySummary input);
        Task UpdateBeforeDeleteSchedule(int scheduleId);
        Task<List<SummaryByDayInWeek>> GetChartSummary();
    }
}
