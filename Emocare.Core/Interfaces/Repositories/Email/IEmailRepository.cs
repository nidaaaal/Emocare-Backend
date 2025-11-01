

using Emocare.Domain.Entities.Email;

namespace Emocare.Domain.Interfaces.Repositories.Email
{
    public interface IEmailRepository:IRepository<VerifiedEmail>
    {
        Task<bool> Verified(string email);
        Task<VerifiedEmail?> GetByEmail(string email);

    }
}
