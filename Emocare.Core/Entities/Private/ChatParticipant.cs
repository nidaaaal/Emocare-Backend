using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Enums.Auth;

namespace Emocare.Domain.Entities.Private
{
    public class ChatParticipant
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Users User { get; set; } = null!;   
        public UserRoles Role { get; set; }

        public Guid ChatSessionId { get; set; }
        public UserChatSession ChatSession { get; set; } = null!;

    }
}
