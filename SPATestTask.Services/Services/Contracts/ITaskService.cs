using System;
using System.Collections.Generic;
using System.Text;
using SPATestTask.Core.DataBase;
using SPATestTask.Services.Dto;

namespace SPATestTask.Services.Services.Contracts
{
    public interface ITaskService : IGeneralService<Task, TaskDto, int>
    {
    }
}
