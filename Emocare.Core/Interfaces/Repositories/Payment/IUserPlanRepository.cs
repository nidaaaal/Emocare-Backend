using Emocare.Domain.Entities.Payment;


namespace Emocare.Domain.Interfaces.Repositories.Payment
{
    public interface IUserPlanRepository:IRepository<UserPlan>
    {
        Task<bool> AnyActivePlan(Guid id);
        Task<UserPlan?> CurrentPlan(Guid id);
    }
}
