using System;
using System.Collections.Generic;
using System.Text;

namespace SPATestTask.Services.Dto
{
    public class TaskDto : IDto<int>
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public UserDto User { get; set; }
        public string TaskName { get; set; }
        public DateTime DueDate { get; set; }

        public bool Completed { get; set; }
    }
}
