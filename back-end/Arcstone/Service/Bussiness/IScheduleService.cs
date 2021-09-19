using Contract;
using Entity.Models;
using Service.DTOs;
using Service.DTOs.ScheduleDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Bussiness
{
    public interface IScheduleService : IAppService
    {
        Task<Schedule> GetById(int id);
        Task<BaseResponse> Create(CreateScheduleInput input);
        Task<List<ScheduleInWeekDto>> GetAllScheduleInWeek();
        Task DeleteScheduleById(int id);
        Task<bool> CheckDuplicateSchedule(CreateScheduleInput input);
        Task<BaseResponse> Update(CreateScheduleInput input);
    }
}
