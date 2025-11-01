using Emocare.Application.DTOs.Habits;
using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Interfaces.Repositories.Habits;
using Emocare.Infrastructure.Persistence;
using Emocare.Shared.Helpers.Api;
using Microsoft.EntityFrameworkCore;


namespace Emocare.Infrastructure.Repositories.Habits
{
    public class HabitHabitCompletionRepository : Repository<HabitCompletion>, IHabitHabitCompletionRepository
    {
        public HabitHabitCompletionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<HabitCompletion?>> GetCompletions(int id) 
            => await _dbSet.OrderBy(x=>x.CompletionDate)
            .Where(x => x.HabitId == id).ToListAsync();
        public async Task RecordCompletion(int id, int count, string notes)
        {
            var date = DateTime.Now;
            var habit = await _context.Habits.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException("habit not found");
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.HabitId == id && x.CompletionDate.Date == date.Date);
            if (entity != null)
            {
                int left = habit.TargetCount - entity.Count;

                if (count <= left)
                    throw new BadRequestException("count Can't be higher");
                
                entity.Count += count;
                entity.Notes = notes ?? entity.Notes;
                await _context.SaveChangesAsync();
            }
            else
            {

                _dbSet.Add(new HabitCompletion
                {
                    HabitId = id,
                    CompletionDate = date,
                    Count = count,
                    Notes = notes
                });
                await _context.SaveChangesAsync();  
            }
        }

        public async Task<IEnumerable<HabitCompletion?>> GetById(int id)
            => await _dbSet.OrderBy(x => x.CompletionDate).Where(x => x.HabitId == id).ToListAsync();

        public async Task<int> GetCount(int id)
        {
            var date = DateTime.Now;

           return await _dbSet.Where(x => x.HabitId == id && x.CompletionDate.Date == date.Date).SumAsync(x => x.Count);
        }


    }
}
