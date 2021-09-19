using Entity.Models;
using Repository;
using Service.Commands;
using Service.DTOs;
using Service.DTOs.ScheduleDTOs;
using Service.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Bussiness
{
    public class ScheduleService : AppService, IScheduleService
    {
        private readonly IScheduleQueries _scheduleQueries;
        private readonly IScheduleCommands _scheduleCommands;
        private readonly IGlobalService _globalService;
        private readonly IWeeklySummaryQueries _weeklySummaryQueries;
        private readonly IWeeklySummaryCommands _weeklySummaryCommands;
        public ScheduleService(IScheduleQueries scheduleQueries,
                                IScheduleCommands scheduleCommands,
                                IGlobalService globalService,
                                IWeeklySummaryQueries weeklySummaryQueries,
                                IWeeklySummaryCommands weeklySummaryCommands)
        {
            _scheduleQueries = scheduleQueries;
            _scheduleCommands = scheduleCommands;
            _globalService = globalService;
            _weeklySummaryQueries = weeklySummaryQueries;
            _weeklySummaryCommands = weeklySummaryCommands;
        }

        public async Task<bool> CheckDuplicateSchedule(CreateScheduleInput input)
        {
            return await _scheduleQueries.CheckDuplicateSchedule(input);
        }

        public async Task<BaseResponse> Create(CreateScheduleInput input)
        {
            var startTime = input.ScheduleDate.Date.AddHours(input.StartHour).AddMinutes(input.StartMinute);
            var endTime = input.ScheduleDate.Date.AddHours(input.EndHour).AddMinutes(input.EndMinute);
            var weekIndex = _globalService.GetWeekIndex(input.ScheduleDate);
            var totalHour = _globalService.GetTotalHour(new List<WorkingTimeDto>() {
                new WorkingTimeDto {
                    StartTime = startTime,
                    EndTime = endTime
                }
            });
            var schedule = new Schedule()
            {
                Description = input.Description,
                EmployeeId = 1,
                StartTime = startTime,
                EndTime = endTime,
                ProjectId = input.ProjectId,
                TotalHours = totalHour,
                WeekIndex = weekIndex,
                Year = startTime.Year
            };
            await _scheduleCommands.Create(schedule);

            var weekyInfo = await _weeklySummaryQueries.FindWeekInYear(weekIndex, startTime.Year);
            if (weekyInfo == null)
            {
                await _weeklySummaryCommands.Create(new WeeklySummary()
                {
                    EmployeeId = 0,
                    TotalHours = totalHour,
                    WeekIndex = weekIndex,
                    Year = startTime.Year
                });
            }
            else
            {
                weekyInfo.TotalHours += totalHour;
                await _weeklySummaryCommands.Update(weekyInfo);
            }

            await Task.CompletedTask;
            return new BaseResponse()
            {
                Status = true
            };
        }

        public async Task DeleteScheduleById(int id)
        {
            var schedule = await _scheduleQueries.GetById(id);
            if (schedule != null)
            {
                await _scheduleCommands.Delete(schedule);
            }
        }

        public async Task<List<ScheduleInWeekDto>> GetAllScheduleInWeek()
        {
            var weekIndex = _globalService.GetWeekIndex(DateTime.Now);
            var lstSchedule = (await _scheduleQueries.GetAllScheduleInWeek(weekIndex, DateTime.Now.Year))
                            .Select(w => new
                            {
                                Schedule = w,
                                ScheduleDate = w.StartTime.DayOfWeek
                            });
            var rs = (from s in lstSchedule
                      group s.Schedule by s.ScheduleDate into G
                      select new ScheduleInWeekDto()
                      {
                          ListSchedules = G.Select(s => s).ToList()
                      }).ToList();
            return rs;
        }

        public async Task<Schedule> GetById(int id)
        {
            return await _scheduleQueries.GetById(id);
        }

        public async Task<BaseResponse> Update(CreateScheduleInput input)
        {
            var oldSchedule = await _scheduleQueries.GetById(input.Id.Value);
            var startTime = input.ScheduleDate.Date.AddHours(input.StartHour).AddMinutes(input.StartMinute);
            var endTime = input.ScheduleDate.Date.AddHours(input.EndHour).AddMinutes(input.EndMinute);
            var weekIndex = _globalService.GetWeekIndex(input.ScheduleDate);
            var totalHour = _globalService.GetTotalHour(new List<WorkingTimeDto>() {
                new WorkingTimeDto {
                    StartTime = startTime,
                    EndTime = endTime
                }
            });

            var oldWeekyInfo = await _weeklySummaryQueries.FindWeekInYear(oldSchedule.WeekIndex.Value, oldSchedule.Year.Value);
            if (oldWeekyInfo != null)
            {
                oldWeekyInfo.TotalHours -= oldSchedule.TotalHours;
                if (oldSchedule.WeekIndex.Value == weekIndex) oldWeekyInfo.TotalHours += totalHour;
                await _weeklySummaryCommands.Update(oldWeekyInfo);
            }

            if (oldSchedule.WeekIndex.Value != weekIndex)
            {
                var newWeekyInfo = await _weeklySummaryQueries.FindWeekInYear(weekIndex, startTime.Year);
                if (newWeekyInfo == null)
                {
                    await _weeklySummaryCommands.Create(new WeeklySummary()
                    {
                        EmployeeId = 0,
                        TotalHours = totalHour,
                        WeekIndex = weekIndex,
                        Year = startTime.Year
                    });
                }
                else
                {
                    newWeekyInfo.TotalHours += totalHour;
                    await _weeklySummaryCommands.Update(newWeekyInfo);
                }
            }
            oldSchedule.Description = input.Description;
            oldSchedule.EmployeeId = 1;
            oldSchedule.StartTime = startTime;
            oldSchedule.EndTime = endTime;
            oldSchedule.ProjectId = input.ProjectId;
            oldSchedule.TotalHours = totalHour;
            oldSchedule.WeekIndex = weekIndex;
            oldSchedule.Year = startTime.Year;
            await _scheduleCommands.Update(oldSchedule);
            return new BaseResponse()
            {
                Status = true
            };
        }

    }
}
