using System;
using System.Collections.Generic;
using System.Text;

namespace SPATestTask.Services.Dto
{
    public class UserDto : IDto<int>
    {
        public UserDto()
        {
            Tasks = new List<TaskDto>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }

        public List<TaskDto> Tasks { get; set; }
    }
}
