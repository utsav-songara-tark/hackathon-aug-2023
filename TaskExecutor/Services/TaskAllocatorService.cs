using TaskStatus = TaskExecutor.Enums.TaskStatus;

namespace TaskExecutor.Services
{
    public class TaskAllocatorService
    {
        private readonly HttpClient _httpClient;
        private readonly WorkerService _workerService;
        private readonly TaskService _taskService;
        private readonly ILogger<TaskAllocatorService> _logger;

        public TaskAllocatorService(WorkerService workerService, TaskService taskService, ILogger<TaskAllocatorService> logger)
        {
            _workerService = workerService;
            _taskService = taskService;
            _httpClient = new HttpClient();
            _logger = logger;
        }

        public async Task AllocateTaskToNode()
        {
            var pendingTask = _taskService.GetPendingTask();
            if (pendingTask != null)
            {
                var node = _workerService.GetAvailableNode();
                if (node != null)
                {
                    pendingTask.Status = TaskStatus.Running;
                    var workerRequestUrl = $"{node.Address}/work?taskId={pendingTask.Id}";
                    _logger.LogInformation($"Sending request to {workerRequestUrl}");
                    await _httpClient.GetAsync(workerRequestUrl);
                }
            }
        }
    }
}
