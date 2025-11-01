using Emocare.Domain.Entities.Habits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Entities.Habits
{
    public class HabitCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ColorCode { get; set; } = string.Empty;
        public ICollection<Habit>? Habits { get; set; }
    }
}
