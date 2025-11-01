using Emocare.Domain.Entities.Private;
using Emocare.Domain.Interfaces.Helper.AiChat;
using Emocare.Domain.Interfaces.Repositories.Payment;
using Emocare.Domain.Interfaces.Repositories.Private;
using Emocare.Shared.Helpers.Api;
using Microsoft.AspNetCore.SignalR;


namespace Emocare.Application.Services
{
    public class PsychologistChatHub:Hub
    {
        
        private readonly IUserFinder _userFinder;
        private readonly IUserPlanRepository _userPlan;
        private readonly IUserChatMessageRepository _chatMessage; 
        private readonly IUserChatSessionRepository  _chatSession;
        private readonly IChatParticipantRepository _chatParticipant;
        public PsychologistChatHub(IUserFinder userFinder, IUserPlanRepository userPlanRepository,
            IUserChatMessageRepository userChat, IUserChatSessionRepository userChatSession, IChatParticipantRepository chatParticipant)
        {
            _chatMessage = userChat;
            _userFinder = userFinder;
            _userPlan = userPlanRepository;
            _chatSession = userChatSession;
            _chatParticipant = chatParticipant;
        }

        public override async Task OnConnectedAsync()
        {
            Guid userId = _userFinder.GetId();
            if (! await _userPlan.AnyActivePlan(userId))
            {
                await Clients.Caller.SendAsync("PlanRequired", "Please purchase a chat plan to continue.");
                Context.Abort();
                return;
            }

            var session = await _chatParticipant.FindSession(userId);

            foreach (var sid in session)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"chat-{sid}");
            }

            await base.OnConnectedAsync();

        }

        public async Task SendMessage(Guid sessionId,string content)
        {
            var userId = _userFinder.GetId();
            var res = await _chatParticipant.FindParticipant(sessionId,userId);

            if (!res) throw new BadRequestException("No Participant Found");

            var receiver = await _chatParticipant.FindReceiver(sessionId, userId);

            Guid receiverId = receiver.FirstOrDefault();

            var message = new UserChatMessage
            {
                ChatSessionId = sessionId,
                SenderId = userId,
                ReceiverId = receiverId,
                Message = content,
                SentAt = DateTime.UtcNow
            };
           await _chatMessage.Add(message);

            var payload = new
            {
                id = message.Id,
                chatSessionId = message.ChatSessionId,
                senderId = message.SenderId,
                receiverId = message.ReceiverId,
                message = message.Message,
                sentAt = message.SentAt
            };

            await Clients.Group($"chat-{sessionId}").SendAsync("ReceiveMessage",payload);
        }
    }
}
