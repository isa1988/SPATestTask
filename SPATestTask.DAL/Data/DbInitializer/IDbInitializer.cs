using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPATestTask.DAL.Data.DbInitializer
{
    public interface IDbInitializer
    {
        public Task Initialize();
    }
}
