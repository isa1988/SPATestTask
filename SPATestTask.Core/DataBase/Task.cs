using System;
using System.Collections.Generic;
using System.Text;

namespace SPATestTask.Core.DataBase
{
    public class Task : IEntity<int>
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public string TaskName { get; set; }
        public DateTime DueDate { get; set; }

        public bool Completed { get; set; }
    }
}
