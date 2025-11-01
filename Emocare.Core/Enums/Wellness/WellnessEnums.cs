using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Enums.Wellness
{
    public enum TaskDifficulty
    {
        Easy = 0,
        Medium = 1,
        Hard = 2
    }
    public enum HabitCategory
    {
        Mindfulness = 0,
        Exercise = 1,
        Journaling = 2,
        Reflection = 3,
        Nutrition = 4,
        Sleep = 5,
        Social = 6,
        Other = 7
    }
    public enum TaskFrequency
    {
        OneTime = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Custom = 4
    }
}
