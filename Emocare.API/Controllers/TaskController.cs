using Emocare.Application.DTOs.Task;
using Emocare.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emocare.API.Controllers
{
    [Route("api/Task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IWellnessTaskService _taskService;
        public TaskController(IWellnessTaskService wellnessTask)
        {
            _taskService = wellnessTask;
        }

        [HttpGet("/all")]
        public async Task<IActionResult> GetAllTask()
            => Ok(await _taskService.GetTasksAsync());

       


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
            => Ok(await _taskService.GetTasksByIdAsync(id));

        [Authorize(Roles ="User")]
        [HttpGet("/today")]
               public async Task<IActionResult> GetTodays()
            => Ok(await _taskService.GetTodays());


        [Authorize(Policy = "AdminOnly")]
        [HttpGet("/admin")]
        public async Task<IActionResult> GetAllByAdminTask()
           => Ok(await _taskService.GetAdminTasksAsync());

        [Authorize(Policy ="AdminOnly")]
        [HttpGet("/psychologists")]
        public async Task<IActionResult> GetAllByPsychologistTask()
           => Ok(await _taskService.GetPsychologistTaskAsync());

        [Authorize(Roles ="Psychologist")]

        [HttpGet("/Psychologist")]
        public async Task<IActionResult> GetByPsychologistIdTask()
          => Ok(await _taskService.GetPsychologistTaskByIdAsync());

        [Authorize(Policy = "Protected")]
        [HttpPost]
        public async Task<IActionResult> AddTask(InsertTaskDto dto)
            => Ok(await _taskService.AddTaskAsync(dto));

        [Authorize(Policy = "Protected")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id,InsertTaskDto dto)
            => Ok(await _taskService.UpdateTaskAsync(id,dto));

        [Authorize(Policy = "Protected")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
            => Ok(await _taskService.DeleteTaskAsync(id));

    }
}
