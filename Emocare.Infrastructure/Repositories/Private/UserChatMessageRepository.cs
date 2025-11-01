using Emocare.Domain.Entities.Private;
using Emocare.Domain.Interfaces.Repositories.Chat;
using Emocare.Domain.Interfaces.Repositories.Private;
using Emocare.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Infrastructure.Repositories.Private
{
    public class UserChatMessageRepository:Repository<UserChatMessage>,IUserChatMessageRepository
    {
        public UserChatMessageRepository(AppDbContext dbContext)  : base (dbContext){ }
    }
}
