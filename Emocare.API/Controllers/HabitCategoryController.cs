using Emocare.Application.DTOs.Habits;
using Emocare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Route("api/habitCategory")]
    [ApiController]
    public class HabitCategoryController : ControllerBase
    {
        private readonly IHabitCategoryService _habitCategoryService;

        public HabitCategoryController(IHabitCategoryService habitCategoryService)
        {
            _habitCategoryService = habitCategoryService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()=> Ok(await _habitCategoryService.GetAll());

        [Authorize(Policy ="AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Add(AddCategory addCategory) => Ok(await _habitCategoryService.AddCategory(addCategory));
    }
}
