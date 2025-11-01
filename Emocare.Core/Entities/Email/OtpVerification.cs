using Emocare.Domain.Entities.Auth;
using System.ComponentModel.DataAnnotations;


namespace Emocare.Domain.Entities.Email
{
    public class OtpVerification
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }=string.Empty;

        [Required]
        [MaxLength(6)]
        public string OtpCode { get; set; } = string.Empty;
        [Required]
        public DateTime ExpirationTime { get; set; }
        
        public bool IsUsed { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
