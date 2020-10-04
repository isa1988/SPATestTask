using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SPATestTask.Core.DataBase;
using SPATestTask.Services.Dto;

namespace SPATestTask.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            UserMapping();
            TaskMapping();
        }
        private void UserMapping()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }

        private void TaskMapping()
        {
            CreateMap<Task, TaskDto>();
            CreateMap<TaskDto, Task>();
        }
    }
}
