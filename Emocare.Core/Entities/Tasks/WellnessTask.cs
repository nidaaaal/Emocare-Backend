using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Enums.AiChat;
using Emocare.Domain.Enums.Habit;
using Emocare.Domain.Enums.Wellness;
using System.ComponentModel.DataAnnotations;

namespace Emocare.Domain.Entities.Tasks
{

    public class WellnessTask
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public Mood MoodTag { get; set; }

        [Required]
        public string ImageUrl { get; set; } = "";

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        public HabitCategory Category { get; set; } = HabitCategory.Mindfulness;
        public TaskDifficulty Difficulty { get; set; } = TaskDifficulty.Easy;
        public int EstimatedDurationMinutes { get; set; } = 10;
        public TaskFrequency Frequency { get; set; } = TaskFrequency.Daily;
        public int TotalCompletions { get; set; } = 0;
        public int SuccessRate { get; set; } = 0;
        public bool IsRecommended { get; set; } = false;
        public int Priority { get; set; } = 0;

        public TaskOwnerType OwnerType { get; set; } = TaskOwnerType.Admin;


        // Psychologist who created the task (nullable if Admin task)
        public Guid? CreatedByPsychologistId { get; set; }
        public Users? CreatedByPsychologist { get; set; }

        public ICollection<PsychologistTaskAssignment> PsychologistTaskAssignments { get; set; } = new List<PsychologistTaskAssignment>();
        public ICollection<UserDailyTask> UserDailyTasks { get; set; } = new List<UserDailyTask>();
    }

}