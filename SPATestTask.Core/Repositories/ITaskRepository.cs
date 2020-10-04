using System;
using System.Collections.Generic;
using System.Text;
using SPATestTask.Core.DataBase;

namespace SPATestTask.Core.Repositories
{
    public interface ITaskRepository : IRepository<Task, int>
    {
    }
}
