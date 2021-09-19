using Contract;
using Entity.Models;
using Service.DTOs.ScheduleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries
{
    public interface IScheduleQueries : IQuery
    {
        Task<List<ScheduleInDayDto>> GetAllScheduleInWeek(int weekInYear, int year);
        Task<Schedule> GetById(int id);
        Task<bool> CheckDuplicateSchedule(CreateScheduleInput input);
    }
}
