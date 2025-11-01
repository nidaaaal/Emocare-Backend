using Emocare.Domain.Entities.Auth;
using System.ComponentModel.DataAnnotations;

namespace Emocare.Domain.Entities.Payment
{
    public class UserPlan
    {
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public Users User { get; set; } = null!;
        [Required]
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

    }
}
