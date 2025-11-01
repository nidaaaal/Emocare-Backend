using Emocare.Domain.Entities.Tasks;
using Emocare.Domain.Enums.AiChat;
using Emocare.Domain.Enums.Habit;
using Emocare.Domain.Interfaces.Repositories.Tasks;
using Emocare.Infrastructure.Persistence;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;


namespace Emocare.Infrastructure.Repositories.Tasks
{
    public class WellnessTaskRepository : Repository<WellnessTask>, IWellnessTaskRepository
    {
        public WellnessTaskRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<IEnumerable<WellnessTask>> GetAllActive() => await _dbSet.Where(x => x.IsActive).ToListAsync();
        public async Task<WellnessTask?> GetById(int id) => await _dbSet.FindAsync(id);

        public async Task<bool> Delete(int id)
        {
            var entity = await GetById(id);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<int>> CountOfTask() => await _dbSet.Where(x => x.IsActive).Select(x => x.Id).ToListAsync();

        public async Task<IEnumerable<WellnessTask>> GetByMood(Mood mood) => await _dbSet.Where(x => x.MoodTag == mood && x.IsActive).ToListAsync();

        public async Task<IEnumerable<WellnessTask>> GetRecentTasks(int count) =>
           await _dbSet.OrderByDescending(x => x.AddedOn)
                       .Take(count)
                       .ToListAsync();

        public async Task<IEnumerable<WellnessTask>> AdminTask()=> await _dbSet.Where(x=>x.OwnerType==TaskOwnerType.Admin).ToListAsync();
        public async Task<IEnumerable<WellnessTask>> PsychologistTask() => await _dbSet.Where(x => x.OwnerType == TaskOwnerType.Psychologist).ToListAsync();

        public async Task<IEnumerable<WellnessTask>> PsychologistIdTask(Guid id) => await _dbSet.Where(x => x.OwnerType == TaskOwnerType.Psychologist && x.CreatedByPsychologistId == id).ToListAsync();
    }
}
