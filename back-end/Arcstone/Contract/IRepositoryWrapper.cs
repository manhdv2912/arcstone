using Contract.IRepo;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
    public interface IRepositoryWrapper
    {
        IProductRepository ProductRepo { get; }
        IProjectRepository ProjectRepo { get; }
        void Save();

        IRepositoryBase<T> Resolve<T>() where T : BaseEntity;
    }
}
