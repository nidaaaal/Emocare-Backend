
using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Enums.Habit;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Emocare.Application.DTOs.Habits
{
    #region Response
    public class HabitStats
    {
        public int TotalCompletions { get; set; }
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public decimal CompletionPercentage { get; set; }
    }

    public class HabitDetails
    {
            public int Id { get; set; }

            public string Name { get; set; } = string.Empty;

            public string Description { get; set; } = string.Empty;

            public int TargetCount { get; set; } = 1;

            public Frequency Frequency { get; set; } = Frequency.Daily;

            public DateTime StartDate { get; set; }

            public DateTime? EndDate { get; set; }

            public TimeSpan? ReminderTime { get; set; }


            public int CategoryId { get; set; } = 1;


            public bool IsEnded { get; set; } = false;
            public bool IsFinished { get; set; } = false;


           public int CompletionCount { get; set; } = 0;



        
    }


    #endregion

    #region Request
    public class AddHabit
    {

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int TargetCount { get; set; } = 1;

        [Required]
        public Frequency Frequency { get; set; } = Frequency.Daily;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public TimeSpan? ReminderTime { get; set; }


        public int CategoryId { get; set; }

    }

    public class CompletionRequest
    {
        public int Count { get; set; }
        public string Notes { get; set; } = "";
    }

    public class AddCategory
    {
        public string Name { get; set; } = string.Empty;
        public string ColorCode { get; set; } = string.Empty;
    }
    #endregion
}
