using System;
using System.Collections.Generic;
using System.Text;

namespace SPATestTask.Core.DataBase
{
    public class User : IEntity<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
