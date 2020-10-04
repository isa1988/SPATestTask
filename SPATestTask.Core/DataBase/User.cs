using System;
using System.Collections.Generic;
using System.Text;

namespace SPATestTask.Core.DataBase
{
    public class User : IEntity<int>
    {
        public User()
        {
            Tasks = new List<Task>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
