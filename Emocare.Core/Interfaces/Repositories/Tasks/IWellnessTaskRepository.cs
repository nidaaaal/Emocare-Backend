using Emocare.Domain.Entities.Tasks;
using Emocare.Domain.Enums.AiChat;
using Emocare.Domain.Enums.Habit;
using Microsoft.EntityFrameworkCore;

namespace Emocare.Domain.Interfaces.Repositories.Tasks
{
    public interface IWellnessTaskRepository : IRepository<WellnessTask>
    {
        Task<WellnessTask?> GetById(int id);
        Task<bool> Delete(int id);
        Task<IEnumerable<WellnessTask>> GetAllActive();

        Task<IEnumerable<int>> CountOfTask();
        Task<IEnumerable<WellnessTask>> GetByMood(Mood mood);
        Task<IEnumerable<WellnessTask>> GetRecentTasks(int count);
        Task<IEnumerable<WellnessTask>> PsychologistTask();
        Task<IEnumerable<WellnessTask>> PsychologistIdTask(Guid id);

        Task<IEnumerable<WellnessTask>> AdminTask();



    }
}
