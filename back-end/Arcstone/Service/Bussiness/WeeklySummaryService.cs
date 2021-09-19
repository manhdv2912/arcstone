using Entity.Models;
using Repository;
using Service.Commands;
using Service.DTOs.ScheduleDTOs;
using Service.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Service.Bussiness
{
    public class WeeklySummaryService : AppService, IWeeklySummaryService
    {
        private readonly IWeeklySummaryQueries _weeklySummaryQueries;
        private readonly IWeeklySummaryCommands _weeklySummaryCommands;
        private readonly IGlobalService _globalService;
        private readonly IScheduleService _scheduleService;
        private readonly IScheduleQueries _scheduleQueries;
        private readonly IProjectService _projectService;


        public WeeklySummaryService(IGlobalService globalService,
                               IWeeklySummaryQueries weeklySummaryQueries,
                               IWeeklySummaryCommands weeklySummaryCommands,
                               IScheduleService scheduleService,
                               IScheduleQueries scheduleQueries,
                               IProjectService projectService)
        {
            _globalService = globalService;
            _weeklySummaryQueries = weeklySummaryQueries;
            _weeklySummaryCommands = weeklySummaryCommands;
            _scheduleService = scheduleService;
            _scheduleQueries = scheduleQueries;
            _projectService = projectService;
        }

        public async Task Create(WeeklySummary input)
        {
            await _weeklySummaryCommands.Create(input);
        }

        public async Task Update(WeeklySummary input)
        {
            await _weeklySummaryCommands.Update(input);
        }

        public async Task<WeeklySummary> FindWeekIndex(int weekIndex, int year)
        {
            return await _weeklySummaryQueries.FindWeekInYear(weekIndex, year);
        }

        public async Task<WeeklySummaryDto> FindCurrentWeek()
        {
            var weekIndex = _globalService.GetWeekIndex(DateTime.Now);
            var weeklySummary = await _weeklySummaryQueries.FindWeekInYear(weekIndex, DateTime.Now.Year);
            if (weeklySummary == null) return new WeeklySummaryDto();
            return new WeeklySummaryDto()
            {
                Id = weeklySummary.Id,
                TotalHours = weeklySummary.TotalHours.Value,
                TotalHoursStr = TimeSpan.FromHours(weeklySummary.TotalHours.Value).ToString(@"hh\:mm"),
                WeekIndex = weeklySummary.EmployeeId
            };
        }

        public async Task UpdateBeforeDeleteSchedule(int scheduleId)
        {
            var schedule = await _scheduleService.GetById(scheduleId);
            if (schedule != null)
            {
                var weeklySummary = await _weeklySummaryQueries.FindWeekInYear(schedule.WeekIndex.Value, schedule.Year.Value);
                if (weeklySummary != null)
                {
                    weeklySummary.TotalHours -= schedule.TotalHours;
                    await _weeklySummaryCommands.Update(weeklySummary);
                }
            }
            await Task.CompletedTask;
        }

        public async Task<List<SummaryByDayInWeek>> GetChartSummary()
        {
            List<DateTime> listDate = _globalService.GetAllDayInCurrentWeek();
            var weekIndex = _globalService.GetWeekIndex(DateTime.Now);
            var lstSchedule = await _scheduleQueries.GetAllScheduleInWeek(weekIndex, DateTime.Now.Year);
            var lstAllProject = await _projectService.GetAll();
            List<SummaryByDayInWeek> rs = new List<SummaryByDayInWeek>();
            var lstScheduleGroupByDay = (from date in listDate
                                         join sche in lstSchedule on date.Day equals sche.Day into G
                                         from sche in G.DefaultIfEmpty()
                                         group new { sche, date } by date into F
                                         select new
                                         {
                                             Day = F.Key,
                                             Schedule = F.Where(w => w.sche != null).Select(w => w.sche).ToList(),
                                             DayStr = F.Key.ToString("dd-MMM-yyyy")
                                         }).OrderBy(w => w.Day).ToList();
            if (lstScheduleGroupByDay.Any())
            {
                foreach (var scheDay in lstScheduleGroupByDay)
                {
                    var summary = new SummaryByDayInWeek()
                    {
                        Day = scheDay.Day,
                        DayStr = scheDay.DayStr,
                        ListProject = new List<SummaryByProjectInDay>()
                    };
                    

                    if (scheDay.Schedule != null && scheDay.Schedule.Any())
                    {
                        var lstScheduleGroupByProject = (from schedule in scheDay.Schedule
                                                         group schedule by schedule.ProjectId into G
                                                         select new SummaryByProjectInDay
                                                         {
                                                             TotalHours = G.Sum(w => w.TotalHours),
                                                             ProjectId = G.Key,
                                                         }).ToList();
                        if (lstScheduleGroupByProject.Any())
                        {
                            foreach (var scheProj in lstScheduleGroupByProject)
                            {
                                summary.ListProject.Add(new SummaryByProjectInDay()
                                {
                                    ProjectId = scheProj.ProjectId,
                                    TotalHours = scheProj.TotalHours
                                });
                            }
                        }
                    }
                    rs.Add(summary);
                }
            }
            return rs;
        }
    }
}
