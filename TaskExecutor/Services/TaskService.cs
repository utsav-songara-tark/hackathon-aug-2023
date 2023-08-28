using Task = TaskExecutor.Models.Task;
using TaskStatus = TaskExecutor.Enums.TaskStatus;

namespace TaskExecutor.Services
{
    public class TaskService
    {
        private readonly List<Task> TaskList = new();

        public List<Task> GetTasks()
        {
            return TaskList.ToList();
        }

        public List<Task> GetTasksByStatus(TaskStatus status)
        {
            return TaskList.Where(x => x.Status == status).ToList();
        }

        public void UpdateTaskStatus(Guid id, string status)
        {
            var task = TaskList.FirstOrDefault(x => x.Id == id);
            task.Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), status, true);
        }

        public void RegisteredTasks()
        {
            var newTask = new Task()
            {
                Id = Guid.NewGuid(),
                Status = TaskStatus.Pending
            };
            TaskList.Add(newTask);
        }

        public Task GetPendingTask()
        {
            return TaskList.FirstOrDefault(x => x.Status != TaskStatus.Completed && x.Status != TaskStatus.Failed);
        }
    }
}
