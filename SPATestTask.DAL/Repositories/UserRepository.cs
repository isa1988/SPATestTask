using System;
using System.Collections.Generic;
using System.Text;
using SPATestTask.Core.DataBase;
using SPATestTask.Core.Repositories;
using SPATestTask.DAL.Data;

namespace SPATestTask.DAL.Repositories
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(DbContextSPATestTask contextSpaTestTask) : base(contextSpaTestTask)
        {
            dbSet = contextSpaTestTask.Users;
        }
    }
}
