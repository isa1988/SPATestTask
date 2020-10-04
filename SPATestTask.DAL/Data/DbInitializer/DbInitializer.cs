using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SPATestTask.Core.DataBase;
using SPATestTask.Core.Repositories;

namespace SPATestTask.DAL.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        public DbInitializer(IServiceScopeFactory serviceScopeProvider, IUserRepository userRepository, ITaskRepository taskRepository)
        {
            this.serviceScopeProvider =
                serviceScopeProvider ?? throw new ArgumentNullException(nameof(serviceScopeProvider));
            this.userRepository =
                userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.taskRepository =
                taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        private IServiceScopeFactory serviceScopeProvider;
        private IUserRepository userRepository;
        private ITaskRepository taskRepository;
        public async System.Threading.Tasks.Task Initialize()
        {
            using (var serviceScope = serviceScopeProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DbContextSPATestTask>();
                context.Database.EnsureCreated();
                var users = new List<User>();
                users.Add(new User { UserName = "Алексеев Станислав Михаилович" });
                users.Add(new User { UserName = "Семёнов Владимир Всеволодович" });
                users.Add(new User { UserName = "Гусев Зиновий Анатольевич" });
                users.Add(new User { UserName = "Исаков Евгений Романович" });
                users.Add(new User { UserName = "Савин Илларион Германнович" });
                users.Add(new User { UserName = "Кошелев Алан Давидович" });
                users.Add(new User { UserName = "Бобылёв Май Тимурович" });
                for (int i = 0; i < users.Count; i++)
                {
                    users[i] = await userRepository.AddAsync(users[i]);
                }
                userRepository.Save();
                await taskRepository.AddAsync(GerTask("Задача 1", new DateTime(2020, 11, 10), users[0], false));
                await taskRepository.AddAsync(GerTask("Задача 2", new DateTime(2020, 10, 9), users[0], true));
                await taskRepository.AddAsync(GerTask("Задача 3", new DateTime(2020, 11, 12), users[0], false));
                await taskRepository.AddAsync(GerTask("Задача 1", new DateTime(2020, 11, 11), users[1], false));
                await taskRepository.AddAsync(GerTask("Задача 2", new DateTime(2020, 10, 8), users[1], true));
                await taskRepository.AddAsync(GerTask("Задача 1", new DateTime(2020, 11, 15), users[2], false));
                await taskRepository.AddAsync(GerTask("Задача 2", new DateTime(2020, 10, 5), users[2], true));
                await taskRepository.AddAsync(GerTask("Задача 1", new DateTime(2020, 11, 20), users[3], false));
                await taskRepository.AddAsync(GerTask("Задача 2", new DateTime(2020, 10, 8), users[3], true));
                taskRepository.Save();
            }
        }

        private Task GerTask(string name, DateTime dueDate, User user, bool completed)
        {
            return new Task {TaskName = name, DueDate = dueDate, UserId = user.Id, Completed = completed};
        }


    }
}
