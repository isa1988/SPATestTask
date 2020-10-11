using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SPATestTask.Core.DataBase;
using SPATestTask.Core.Repositories;
using SPATestTask.Services.Dto;
using SPATestTask.Services.Services.Contracts;

namespace SPATestTask.Services.Services
{
    public class UserService : GeneralService<User, UserDto, int>, IUserService
    {
        public UserService(IMapper mapper, IUserRepository repository) : base(new UserDto(), mapper, repository)
        {
        }

        protected override string CheckBeforeModification(UserDto value, bool isNew = true)
        {
            string error = string.Empty;
            if (string.IsNullOrWhiteSpace(value.UserName))
                error = "Не запоненно ФИО";
            return error;
        }

        protected override string CkeckBeforeDelete(User value)
        {
            return string.Empty;
        }
    }
}
