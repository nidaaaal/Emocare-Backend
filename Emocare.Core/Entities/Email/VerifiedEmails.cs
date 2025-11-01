

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emocare.Domain.Entities.Email
{
    public class VerifiedEmail
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public bool IsVerified { get; set; } = false;

        public DateTime? VerifiedOn { get; set; }
    }
}
