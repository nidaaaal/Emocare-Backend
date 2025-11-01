using Emocare.Domain.Entities.Payment;

namespace Emocare.Domain.Interfaces.Repositories.Payment
{
    public interface IPlanRepository:IRepository<Plan>
    {
       Task<Plan?> GetById(int id);
       Task<bool> GetByName(string name);
       Task<bool> GetByPrice(decimal price);
    }
}
