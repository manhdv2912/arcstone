using Contract;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CommandRepository<T> : ICommandRepository<T> where T : BaseEntity
    {
        private readonly IRepositoryWrapper _repoWrapper;
        public CommandRepository(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public async Task Create(T entity)
        {
           _repoWrapper.Resolve<T>().Create(entity);
           _repoWrapper.Save();
           await Task.CompletedTask;
        }
        public async Task Update(T entity)
        {
            _repoWrapper.Resolve<T>().Update(entity);
            _repoWrapper.Save();
            await Task.CompletedTask;
        }

        public async Task Delete(T entity)
        {
            _repoWrapper.Resolve<T>().Delete(entity);
            _repoWrapper.Save();
            await Task.CompletedTask;
        }

    }
}
