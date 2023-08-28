using TaskStatus = TaskExecutor.Enums.TaskStatus;

namespace TaskExecutor.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public TaskStatus Status { get; set; }
    }
}
