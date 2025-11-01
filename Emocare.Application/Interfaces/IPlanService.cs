using Emocare.Application.DTOs.Plan;
using Emocare.Domain.Entities.Payment;
using Emocare.Shared.Helpers.Api;


namespace Emocare.Application.Interfaces
{
    public interface IPlanService
    {
        Task<ApiResponse<IEnumerable<Plan>>> GetPlan();
        Task<ApiResponse<GetPlan>?> GetPlanById(int id);
        Task<ApiResponse<string>> AddPlan(AddPlan plan);
        Task<ApiResponse<string>?> UpdatePlan(int id,AddPlan plan);
        Task<ApiResponse<string>?> DeletePlan(int id);

    }
}
