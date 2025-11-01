using System;
using System.ComponentModel.DataAnnotations;

namespace Emocare.Domain.Entities.Payment
{
    public class Plan
    {
        public int Id {  get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }=string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Duration { get; set; }   

        public bool IsDelete { get; set; } = false;

        public ICollection<UserPlan> UserPlans { get; set; } = new List<UserPlan>();    
    }
}
