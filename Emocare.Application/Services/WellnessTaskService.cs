using AutoMapper;
using Emocare.Application.DTOs.Task;
using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Entities.Tasks;
using Emocare.Domain.Enums.AiChat;
using Emocare.Domain.Enums.Habit;
using Emocare.Domain.Interfaces.Extension;
using Emocare.Domain.Interfaces.Helper.AiChat;
using Emocare.Domain.Interfaces.Repositories.Chat;
using Emocare.Domain.Interfaces.Repositories.Tasks;
using Emocare.Domain.Interfaces.Repositories.User;
using Emocare.Shared.Helpers.Api;

namespace Emocare.Application.Services
{
    public class WellnessTaskService : IWellnessTaskService
    {
        private readonly IWellnessTaskRepository _taskRepository;
        private readonly IUserDailyTaskRepository _userDailyTaskRepository;
        private readonly IJournalEntryRepository _journalEntry;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IUserFinder _userFinder;
        private readonly IMapper _mapper;

        public WellnessTaskService(
            IWellnessTaskRepository taskRepository,
            IUserDailyTaskRepository userDailyTask,
            IMapper mapper,
            IJournalEntryRepository journal,
            ICloudinaryService cloudinaryService,
            IUserFinder finder)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _journalEntry = journal;
           _cloudinaryService = cloudinaryService;
            _userFinder = finder;
            _userDailyTaskRepository = userDailyTask;
        }

        public async Task<ApiResponse<IEnumerable<WellnessTask>>> GetTasksAsync()
        {
            var tasks = await _taskRepository.GetAllActive();
            return ResponseBuilder.Success(tasks, "Task Fetched Successfully", "GetTasks");
        }

        public async Task<ApiResponse<WellnessTask>?> GetTasksByIdAsync(int id)
        {
            var task = await _taskRepository.GetById(id)
                ?? throw new NotFoundException("No Task Found For The Given Id");

            return ResponseBuilder.Success(task, "Task Fetched Successfully", "GetTasksByIdAsync");
        }
        public async Task<ApiResponse<IEnumerable<WellnessTask>>> GetAdminTasksAsync()
        {
            var tasks = await _taskRepository.AdminTask();
            return ResponseBuilder.Success(tasks, "Task Fetched Successfully", "GetTasks");
        }

        public async Task<ApiResponse<IEnumerable<WellnessTask>>> GetPsychologistTaskAsync()
        {
            var tasks = await _taskRepository.PsychologistTask();
            return ResponseBuilder.Success(tasks, "Task Fetched Successfully", "GetTasks");
        }

        public async Task<ApiResponse<IEnumerable<WellnessTask>>> GetPsychologistTaskByIdAsync()
        {
            Guid id = _userFinder.GetId();
            var tasks = await _taskRepository.PsychologistIdTask(id);
            return ResponseBuilder.Success(tasks, "Task Fetched Successfully", "GetTasks");
        }



        public async Task<ApiResponse<string>> AddTaskAsync(InsertTaskDto dto)
        {
            if (dto.Image == null) return ResponseBuilder.Fail<string>("image required", "TaskService", 500);

            var task = _mapper.Map<WellnessTask>(dto);
            if (dto.OwnerType == TaskOwnerType.Psychologist)
            {
                Guid psyId = _userFinder.GetId();
                task.CreatedByPsychologistId = psyId;
            }
            string url = await _cloudinaryService.UploadImage(dto.Image, $"wellnessTask/${task.Id}");
            task.ImageUrl = url;
            await _taskRepository.Add(task);

            return ResponseBuilder.Success("Task Added", "New Task Added to WellnessTask", "AddTaskAsync");
        }

        public async Task<ApiResponse<string>?> UpdateTaskAsync(int id, InsertTaskDto dto)
        {
            var task = await _taskRepository.GetById(id)
                ?? throw new NotFoundException("No Task Found For The Given Id");


            task.Title = dto.Title;
            task.Description = dto.Description;
            task.MoodTag = dto.MoodTag;
            task.OwnerType = dto.OwnerType;
            task.Category = dto.Category;
            task.Difficulty = dto.Difficulty;
            task.EstimatedDurationMinutes = dto.EstimatedDurationMinutes;
            task.Frequency = dto.Frequency;
            task.Priority = dto.Priority;
            task.IsRecommended = dto.IsRecommended;

            if (dto.Image != null)
            {
                if (await _cloudinaryService.DeleteImageAsync(task.ImageUrl))
                {
                    string url = await _cloudinaryService.UploadImage(dto.Image, $"wellnessTask/${id}");
                    task.ImageUrl = url;
                }
            }

            bool updated = await _taskRepository.Update(task);
            if (!updated)
                return ResponseBuilder.Fail<string>("Task Updating Failed", "UpdateTaskAsync", 500);

            return ResponseBuilder.Success("Task Updated", "Updated Task in WellnessTask", "UpdateTaskAsync");
        }

        public async Task<ApiResponse<string>?> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetById(id)
                ?? throw new NotFoundException("No Task Found For The Given Id");

            bool deleted = await _taskRepository.Delete(id);
            if (!deleted)
                return ResponseBuilder.Fail<string>("Task Delete Failed", "DeleteTaskAsync", 500);

            return ResponseBuilder.Success("Task Deleted Successfully", "Deleted Task from WellnessTask", "DeleteTaskAsync");
        }

        public async Task<ApiResponse<WellnessTask>> GetTodays()
        {
            var userId = _userFinder.GetId();

            // ✅ Check if the user already has a task for today
            var todayTask = await _userDailyTaskRepository.TodayTask(userId);

            if (todayTask != null)
            {
                return ResponseBuilder.Success(
                    todayTask.WellnessTask,
                    "Today's Task Retrieved Successfully (Existing)",
                    "GetTodays"
                );
            }

            // ✅ Fetch random new task
            var tasks = await _taskRepository.GetAll();
            var randomTask = tasks.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

            if (randomTask == null)
                return ResponseBuilder.Fail<WellnessTask>(
                    "No tasks found",
                    "GetTodays",
                    404
                );

            // ✅ Save as today's task
            var newUserTask = new UserDailyTask
            {
                UserId = userId,
                WellnessTaskId = randomTask.Id,
                DateAssigned = DateTime.UtcNow
            };

            await _userDailyTaskRepository.Add(newUserTask);

            return ResponseBuilder.Success(
                randomTask,
                "Today's Task Assigned Successfully",
                "GetTodays"
            );
        }

    }
}
