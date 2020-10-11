using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SPATestTask.Core.DataBase;
using SPATestTask.Core.Repositories;
using SPATestTask.Services.Dto;
using SPATestTask.Services.Services.Contracts;

namespace SPATestTask.Services.Services
{
    public class TaskService : GeneralService<Task, TaskDto, int>, ITaskService
    {
        public TaskService(IMapper mapper, ITaskRepository repository) : base(new TaskDto(), mapper, repository)
        {
        }

        protected override string CheckBeforeModification(TaskDto value, bool isNew = true)
        {
            string error = string.Empty;
            if (string.IsNullOrWhiteSpace(value.TaskName))
                return error = "Не заполнено наименование";
            return error;
        }

        protected override string CkeckBeforeDelete(Task value)
        {
            throw new NotImplementedException();
        }
    }
}
