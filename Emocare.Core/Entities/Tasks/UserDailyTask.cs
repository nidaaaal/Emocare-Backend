using Emocare.Domain.Entities.Auth;


namespace Emocare.Domain.Entities.Tasks
{
    public class UserDailyTask
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public Users User { get; set; } = null!;

        public int WellnessTaskId { get; set; }
        public WellnessTask WellnessTask { get; set; } = null!;

        public DateTime DateAssigned { get; set; } = DateTime.UtcNow;

        public DateTime? DateCompleted { get; set; }

        public bool IsCompleted => DateCompleted.HasValue;

        public void MarkCompleted()
        {
            if (!IsCompleted)
                DateCompleted = DateTime.UtcNow;
        }
    }
}
