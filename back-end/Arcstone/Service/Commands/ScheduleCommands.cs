using Contract;
using Entity.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Commands
{
    public class ScheduleCommands : Command, IScheduleCommands
    {
        private readonly ICommandRepository<Schedule> _scheduleCommandRepository;

        public ScheduleCommands(ICommandRepository<Schedule> scheduleCommandRepository)
        {
            _scheduleCommandRepository = scheduleCommandRepository;
        }
        public async Task Create(Schedule entity)
        {
            await _scheduleCommandRepository.Create(entity);
        }

        public async Task Delete(Schedule entity)
        {
            await _scheduleCommandRepository.Delete(entity);
        }

        public async Task Update(Schedule entity)
        {
            await _scheduleCommandRepository.Update(entity);
        }
    }
}
