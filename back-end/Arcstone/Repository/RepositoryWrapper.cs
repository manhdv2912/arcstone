using Contract;
using Contract.IRepo;
using Entity.Models;
using Repository.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ArcstoneContext _arcstoneContext;
        private IProductRepository _productRepo;
        private IProjectRepository _projectRepo;
        private IScheduleRepository _scheduleRepo;
        private IWeeklySummaryRepository _weeklySummaryRepo;


        public IProductRepository ProductRepo
        {
            get
            {
                if (_productRepo == null)
                {
                    _productRepo = new ProductRepository(_arcstoneContext);
                }
                return _productRepo;
            }
        }

        public IProjectRepository ProjectRepo
        {
            get
            {
                if (_projectRepo == null)
                {
                    _projectRepo = new ProjectRepository(_arcstoneContext);
                }
                return _projectRepo;
            }
        }

        public IScheduleRepository ScheduleRepo
        {
            get
            {
                if (_scheduleRepo == null)
                {
                    _scheduleRepo = new ScheduleRepository(_arcstoneContext);
                }
                return _scheduleRepo;
            }
        }

        public IWeeklySummaryRepository WeeklySummaryRepo
        {
            get
            {
                if (_weeklySummaryRepo == null)
                {
                    _weeklySummaryRepo = new WeeklySummaryRepository(_arcstoneContext);
                }
                return _weeklySummaryRepo;
            }
        }

        public RepositoryWrapper(ArcstoneContext arcstoneContext)
        {
            _arcstoneContext = arcstoneContext;
        }

        public void Save()
        {
            _arcstoneContext.SaveChanges();
        }

        public IRepositoryBase<T> Resolve<T>() where T : BaseEntity
        {
            return new RepositoryBase<T>(_arcstoneContext);
        }
    }
}
