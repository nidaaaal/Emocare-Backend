using Emocare.Application.Interfaces;
using Emocare.Domain.Interfaces.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Application.Services
{
    public class PsychologistChatService : IPsychologistChatService
    {
        private readonly IPsychologistRepository _psychologistRepository;

        public PsychologistChatService(IPsychologistRepository psychologistRepository)
        {
            _psychologistRepository = psychologistRepository;
        }

        
    }
}
