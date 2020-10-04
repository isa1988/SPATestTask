using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPATestTask.API.Models.User;

namespace SPATestTask.API.Models.Task
{
    public class TaskDeleteModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public UserModel User { get; set; }
        public string TaskName { get; set; }
        public DateTime DueDate { get; set; }

        public bool Completed { get; set; }
    }
}
