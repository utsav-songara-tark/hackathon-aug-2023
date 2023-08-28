using TaskStatus = TaskExecutor.Enums.TaskStatus;
using Microsoft.AspNetCore.Mvc;
using TaskExecutor.Models;
using TaskExecutor.Services;

namespace TaskExecutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly TaskService _taskService;
        private readonly TaskAllocatorService _taskAllocatorService;

        public TaskController(TaskService taskService, TaskAllocatorService taskAllocatorService)
        {
            _taskService = taskService;
            _taskAllocatorService = taskAllocatorService;
        }
        
        [HttpPost]
        [Route("register")]
        public IActionResult RegisterTask()
        {
            _taskService.RegisteredTasks();
            return Ok();
        }

        [HttpGet]
        [Route("status")]
        public IActionResult GetTaskStatus()
        {
            return Ok(_taskService.GetTasks());
        }

        [HttpGet]
        [Route("status/{status}")]
        public IActionResult GetTasksByStatus(string status)
        {
            return Ok(_taskService.GetTasksByStatus((TaskStatus)Enum.Parse(typeof(TaskStatus), status, true)));
        }

        [HttpGet]
        [Route("pending")]
        public IActionResult GetPendingTask()
        {
            var task = _taskService.GetPendingTask();
            if (task == null)
            {
                return Ok("No pending tasks");
            }
            return Ok(task);
        }

        [HttpPut]
        [Route("status/{id}")]
        public IActionResult UpdateTaskStatus(string id, [FromBody] TaskStatusUpdateRequest updateRequest)
        {
            _taskService.UpdateTaskStatus(Guid.Parse(id), updateRequest.Status);
            return Ok();
        }

        [HttpPost]
        [Route("allocate")]
        public async Task<IActionResult> AllocateTaskAsync()
        {
            await _taskAllocatorService.AllocateTaskToNode();
            return Ok();
        }
    }
}
