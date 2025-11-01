using Emocare.Application.DTOs.Plan;
using Emocare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Route("api/plan")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()=>Ok(await _planService.GetPlan());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _planService.GetPlanById(id));
        [Authorize(Policy = "AdminOnly")]

        [HttpPost]
        public async Task<IActionResult> Add(AddPlan dto) => Ok(await _planService.AddPlan(dto));
        [Authorize(Policy = "AdminOnly")]

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,AddPlan dto) => Ok(await _planService.UpdatePlan(id,dto));
        [Authorize(Policy = "AdminOnly")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _planService.DeletePlan(id));
    }
}
