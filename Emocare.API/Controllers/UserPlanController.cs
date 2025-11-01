using Emocare.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Route("api/userPlan")]
    [ApiController]
    public class UserPlanController : ControllerBase
    {
        private readonly IUserPlanService  _userPlanService;
         public UserPlanController(IUserPlanService userPlanService)
        {
            _userPlanService = userPlanService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _userPlanService.CurrentPlan());

        [HttpPost]
        public async Task<IActionResult> Activate(int planId) => Ok(await _userPlanService.ActivatePlan(planId));

    }
}
