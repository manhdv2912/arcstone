using Contract;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Commands
{
    public interface IScheduleCommands : ICommand
    {
        Task Create(Schedule entity);
        Task Update(Schedule entity);
        Task Delete(Schedule entity);

    }
}
