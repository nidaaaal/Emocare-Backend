using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Enums.Auth;
using System.ComponentModel.DataAnnotations;


namespace Emocare.Domain.Entities.Private
{
    public class UserChatMessage
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "SenderId is required.")]
        public Guid SenderId { get; set; }
        public Users Sender { get; set; } = null!;
        public UserRoles SenderRole { get; set; }



        [Required(ErrorMessage = "ReceiverId is required.")]
        public Guid ReceiverId { get; set; }
        public Users Receiver { get; set; } =null!;


        [Required(ErrorMessage = "Message is required.")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Message must be between 1 and 1000 characters.")]
        public string Message { get; set; } = string.Empty;


        [Required]
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; } = false;


        [Required(ErrorMessage = "ChatSessionId is required.")]
        public Guid ChatSessionId { get; set; }

        public UserChatSession ChatSession { get; set; } = null!;
    }

}
