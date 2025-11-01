using Emocare.Domain.Entities.Private;
using Emocare.Domain.Interfaces.Repositories.Private;
using Emocare.Infrastructure.Persistence;

namespace Emocare.Infrastructure.Repositories.Private
{
    public class UserChatSessionRepository : Repository<UserChatSession>, IUserChatSessionRepository
    {
        public UserChatSessionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
