
namespace Emocare.Domain.Entities.Private
{
    public class UserChatSession
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ChatParticipant> Participants { get; set; } = new List<ChatParticipant>();

        public ICollection<UserChatMessage> Messages { get; set; } = new List<UserChatMessage>();
    }
}