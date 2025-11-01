using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Entities.Tasks;
using Emocare.Domain.Interfaces.Repositories.Tasks;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Infrastructure.Repositories.Tasks
{
    // Infrastructure/Repositories/UserDailyTaskRepository.cs
    public class UserDailyTaskRepository : Repository<UserDailyTask>, IUserDailyTaskRepository
    {
        public UserDailyTaskRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<UserDailyTask>> GetRecentTasksAsync(Guid userId, int days)
        {
            var fromDate = DateTime.UtcNow.AddDays(-days);
            return await _context.UserDailyTasks
                .Where(t => t.UserId == userId && t.DateAssigned >= fromDate)
                .ToListAsync();
        }
        public async Task<UserDailyTask?> TodayTask(Guid userId)
            => await _dbSet.FirstOrDefaultAsync(
                x => x.UserId == userId && x.DateAssigned.Date == DateTime.UtcNow.Date);

    }

}
