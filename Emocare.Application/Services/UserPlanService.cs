using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Payment;
using Emocare.Domain.Interfaces.Helper.AiChat;
using Emocare.Domain.Interfaces.Repositories.Payment;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Services
{
    public class UserPlanService : IUserPlanService
    {
        private readonly IUserPlanRepository _userPlanRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IUserFinder _userFinder;


        public UserPlanService(IUserPlanRepository userPlan,IPlanRepository planRepository,IUserFinder userFinder)
        { 
            _userPlanRepository = userPlan; 
            _planRepository = planRepository;
            _userFinder = userFinder;
        }

        public async Task<ApiResponse<string>> ActivatePlan(int planId)

        {
            var plan  = await _planRepository.GetById(planId) ?? throw new NotFoundException("Plan id not found");
            DateTime date = DateTime.UtcNow;
            Guid userId = _userFinder.GetId();

          await  _userPlanRepository.Add(new UserPlan
            {UserId = userId,PlanId = planId,StartDate=date,EndDate=date.AddDays(plan.Duration), IsActive=true}
          );
            return ResponseBuilder.Success("Plan Activated", "New Plan Added To User", "UserPlanService");

        }

        public async Task<ApiResponse<UserPlan>> CurrentPlan()
        {
            Guid userId = _userFinder.GetId();

            bool res = await _userPlanRepository.AnyActivePlan(userId);
            if (!res) return ResponseBuilder.Fail<UserPlan>("No Plan Activated", "UserPlanService",404);

            UserPlan plan = await _userPlanRepository.CurrentPlan(userId) ?? throw new NotFoundException("Plan not found for current user");
            return ResponseBuilder.Success(plan, "Plan Fetched Successfully", "UserPlanService");
        }
    }
}
