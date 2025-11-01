using Emocare.Domain.Entities.Auth;


namespace Emocare.Domain.Entities.Tasks
{
    public class UserStreak
    {
            public int Id { get; set; }
            public Guid UserId { get; set; }
            public Users? Users { get; set; }
            public int CurrentStreak { get; set; }
            public DateTime LastUpdated { get; set; }
    }
}
