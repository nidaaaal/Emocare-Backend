using Emocare.Domain.Enums.AiChat;
using Emocare.Domain.Enums.Habit;
using Emocare.Domain.Enums.Wellness;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Emocare.Application.DTOs.Task
{
    public class InsertTaskDto
    {
        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(500)]
        public string Description { get; set; } = string.Empty;

        // Optional image upload
        public IFormFile? Image { get; set; }

        [Required]
        public Mood MoodTag { get; set; }

        [Required]
        public TaskOwnerType OwnerType { get; set; }

        // 🔑 Enhancements
        [Required]
        public HabitCategory Category { get; set; } = HabitCategory.Mindfulness;

        [Required]
        public TaskDifficulty Difficulty { get; set; } = TaskDifficulty.Easy;

        [Range(1, 180, ErrorMessage = "Duration must be between 1 and 180 minutes.")]
        public int EstimatedDurationMinutes { get; set; } = 10;

        [Required]
        public TaskFrequency Frequency { get; set; } = TaskFrequency.Daily;

        [Range(0, 5, ErrorMessage = "Priority must be between 0 and 5.")]
        public int Priority { get; set; } = 0;

        public bool IsRecommended { get; set; } = false;
    }
}
