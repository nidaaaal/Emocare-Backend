using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Enums.Habit;


namespace Emocare.Domain.Entities.Tasks
{
    public class PsychologistTaskAssignment
    {
        public int Id { get; set; }

        public Guid PsychologistId { get; set; }
        public Users Psychologist { get; set; } = null!;

        public Guid ClientId { get; set; }
        public Users Client { get; set; } = null!;

        public int WellnessTaskId { get; set; }
        public WellnessTask WellnessTask { get; set; } = null!;
        public DateTime AssignedOn { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<UserDailyTaskByPsychologist> UserDailyTasks { get; set; } = new List<UserDailyTaskByPsychologist>();
    }

    public class UserDailyTaskByPsychologist
    {
        public int Id { get; set; }

        public int PsychologistTaskAssignmentId { get; set; }
        public PsychologistTaskAssignment PsychologistTaskAssignment { get; set; } = null!;

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
