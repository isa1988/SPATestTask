using System;
using System.Collections.Generic;
using System.Text;

namespace SPATestTask.DAL.Unit.Contracts
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork GetUnitOfWork();
    }
}
