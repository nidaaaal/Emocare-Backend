using Emocare.Domain.Entities.Private;
using Emocare.Domain.Interfaces.Repositories.Private;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Infrastructure.Repositories.Private
{
    public class ChatParticipantRepository:Repository<ChatParticipant>,IChatParticipantRepository
    {
        public ChatParticipantRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Guid>> FindSession(Guid userId) => await
            _dbSet.Where(x => x.UserId==userId).Select(x => x.ChatSessionId).ToListAsync();

        public async Task<bool> FindParticipant(Guid sessionId, Guid userId) => await
            _dbSet.AnyAsync(x => x.ChatSessionId == sessionId && x.UserId == userId);
        public async Task<IEnumerable<Guid>> FindReceiver(Guid sessionId, Guid userId) => await
           _dbSet.Where(x => x.ChatSessionId == sessionId && x.UserId != userId).Select(x => x.UserId).ToListAsync();

    }
}
