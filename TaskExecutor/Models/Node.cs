using TaskExecutor.Enums;

namespace TaskExecutor.Models
{
    public class Node : NodeRegistrationRequest
    {
        public NodeStatus Status { get; set; }
    }
}
