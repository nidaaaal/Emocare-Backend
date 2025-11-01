using Emocare.Domain.Entities.Private;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Interfaces.Repositories.Private
{
    public interface IChatParticipantRepository:IRepository<ChatParticipant>
    {
        Task<IEnumerable<Guid>> FindSession(Guid userId);
        Task<bool> FindParticipant(Guid sessionId,Guid userId);
        Task<IEnumerable<Guid>> FindReceiver(Guid sessionId, Guid userId);
    }
}
