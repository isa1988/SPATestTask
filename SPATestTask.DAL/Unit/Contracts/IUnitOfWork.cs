using System;
using System.Collections.Generic;
using System.Text;
using SPATestTask.Core.Repositories;

namespace SPATestTask.DAL.Unit.Contracts
{
    public interface IUnitOfWork 
    {
        IUserRepository User { get; }
        ITaskRepository Task { get; }
        void RollbackTransaction();
        void CommitTransaction();
    }
}
