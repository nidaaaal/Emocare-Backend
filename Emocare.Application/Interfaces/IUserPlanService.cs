
using Emocare.Domain.Entities.Payment;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Interfaces
{
    public interface IUserPlanService
    {
        Task<ApiResponse<string>> ActivatePlan(int planId);
        Task<ApiResponse<UserPlan>> CurrentPlan();
    }
}
