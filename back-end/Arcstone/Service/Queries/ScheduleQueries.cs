using Contract;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.DTOs.ScheduleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries
{
    public class ScheduleQueries : Query, IScheduleQueries
    {
        private readonly IQueryRepository<Schedule> _scheduleQueriesRepository;
        private readonly IQueryRepository<Project> _projectQueriesRepository;
        public ScheduleQueries(IQueryRepository<Schedule> scheduleQueriesRepository,
                                IQueryRepository<Project> projectQueriesRepository)
        {
            _scheduleQueriesRepository = scheduleQueriesRepository;
            _projectQueriesRepository = projectQueriesRepository;
        }

        public async Task<bool> CheckDuplicateSchedule(CreateScheduleInput input)
        {
            var startTime = input.ScheduleDate.Date.AddHours(input.StartHour).AddMinutes(input.StartMinute);
            var endTime = input.ScheduleDate.Date.AddHours(input.EndHour).AddMinutes(input.EndMinute);
            var lstScheduleTimeInDay = await _scheduleQueriesRepository.FindByCondition(w => input.ScheduleDate.Date == w.StartTime.Value.Date 
                                                                                        && (input.Id == null
                                                                                            || (input.Id.HasValue && w.Id != input.Id)))
                                                                        .OrderBy(w => w.StartTime)
                                                                        .Select(w => new { 
                                                                            StartTime = w.StartTime,
                                                                            EndTime = w.EndTime
                                                                        }).ToListAsync();
            if (lstScheduleTimeInDay == null) return false;
            var lstTime = new List<DateTime>();
            foreach (var item in lstScheduleTimeInDay)
            {
                lstTime.Add(item.StartTime.Value);
                lstTime.Add(item.EndTime.Value);
            }
            var lengthOfLstTime = lstTime.Count();
            for (int i = 0; i < lengthOfLstTime; i++)
            {
                var nextIndex = i + 1;
                if (i == 0 && lstTime[0] > endTime)
                {
                    return false;
                }
                if (nextIndex == lengthOfLstTime && lstTime[lengthOfLstTime - 1] < startTime)
                {
                    return false;
                }

                if (i % 2 == 0 && lstTime[i] == startTime)
                {
                    return true;
                }

                if (nextIndex < lengthOfLstTime && i % 2 == 1 && lstTime[nextIndex] == endTime)
                {
                    return true;
                }

                if (lstTime[i] > startTime && lstTime[i] < endTime)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<ScheduleInDayDto>> GetAllScheduleInWeek(int weekIndex, int year)
        {
            var rs = await (from schedule in _scheduleQueriesRepository.GetAll()
                            join project in _projectQueriesRepository.GetAll() on schedule.ProjectId equals project.Id
                            where schedule.StartTime >= DateTime.Now.Date
                                && schedule.WeekIndex == weekIndex
                                && schedule.Year == year
                            orderby schedule.StartTime
                            select new ScheduleInDayDto()
                            {
                                Id = schedule.Id,
                                Description = schedule.Description,
                                EndTime = schedule.EndTime.Value,
                                StartTime = schedule.StartTime.Value,
                                TotalHours = schedule.TotalHours.Value,
                                ProjectName = project.ProjectName,
                                EndTimeStr = schedule.EndTime.Value.ToString("HH:mm"),
                                StartTimeStr = schedule.StartTime.Value.ToString("HH:mm"),
                                ProjectId = project.Id,
                                StartHour = schedule.StartTime.Value.Hour,
                                EndHour = schedule.EndTime.Value.Hour,
                                StartMinute = schedule.StartTime.Value.Minute,
                                EndMinute = schedule.EndTime.Value.Minute,
                                Year = schedule.StartTime.Value.Year,
                                Month = schedule.StartTime.Value.Month,
                                Day = schedule.StartTime.Value.Day,
                            }).ToListAsync();

            return rs;
        }

        public async Task<Schedule> GetById(int id)
        {
            return await _scheduleQueriesRepository.GetByID(id);
        }
    }
}
