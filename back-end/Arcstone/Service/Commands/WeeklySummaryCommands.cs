using Contract;
using Entity.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Commands
{
    public class WeeklySummaryCommands : Command, IWeeklySummaryCommands
    {
        private readonly ICommandRepository<WeeklySummary> _weeklySummaryCommandRepository;
        public WeeklySummaryCommands(ICommandRepository<WeeklySummary> weeklySummaryCommandRepository)
        {
            _weeklySummaryCommandRepository = weeklySummaryCommandRepository;
        }
        public async Task Create(WeeklySummary entity)
        {
            await _weeklySummaryCommandRepository.Create(entity);
        }

        public async Task Update(WeeklySummary entity)
        {
            await _weeklySummaryCommandRepository.Update(entity);
        }
    }
}
