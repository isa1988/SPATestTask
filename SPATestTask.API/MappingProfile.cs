using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SPATestTask.API.Models.Task;
using SPATestTask.API.Models.User;
using SPATestTask.Services.Dto;

namespace SPATestTask.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            UserMapping();
            TaskMapping();
        }

        private void TaskMapping()
        {
            CreateMap<TaskAddModel, TaskDto>();
            CreateMap<TaskUpdateModel, TaskDto>();
            CreateMap<TaskDeleteModel, TaskDto>();
            CreateMap<TaskModel, TaskDto>();
            CreateMap<TaskDto, TaskModel>();
        }

        private void UserMapping()
        {
            CreateMap<UserAddModel, UserDto>();
            CreateMap<UserUpdateModel, UserDto>();
            CreateMap<UserDeleteModel, UserDto>();
            CreateMap<UserModel, UserDto>();
            CreateMap<UserDto, UserModel>();
        }
    }
}
