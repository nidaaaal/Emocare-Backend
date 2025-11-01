using Emocare.Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Interfaces.Repositories.Tasks
{
    public interface IUserDailyTaskRepository : IRepository<UserDailyTask>
    {
        Task<IEnumerable<UserDailyTask>> GetRecentTasksAsync(Guid userId, int days);
        Task<UserDailyTask> TodayTask(Guid userId);
    }

}
