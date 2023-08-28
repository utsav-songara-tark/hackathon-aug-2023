using TaskExecutor.Enums;
using TaskExecutor.Models;

namespace TaskExecutor.Services
{
    public class WorkerService
    {
        private readonly List<Node> Nodes = new();

        public void RegisterNode(NodeRegistrationRequest request)
        {
            Nodes.Add(new Node
            {
                Name = request.Name,
                Address = request.Address,
                Status = NodeStatus.Available
            });
        }

        public void UnregisterNode(string name)
        {
            var node = Nodes.FirstOrDefault(x => x.Name == name);
            if (node != null)
            {
                Nodes.Remove(node);
            }
        }

        public Node GetAvailableNode()
        {
            var availableNode = Nodes.FirstOrDefault(x => x.Status == NodeStatus.Available);
            if (availableNode != null)
            {
                return availableNode;
            }
            return availableNode;
        }

        public void UpdateNodeStatus(string name, string status)
        {
            var node = Nodes.FirstOrDefault(x => x.Name == name);
            node.Status = (NodeStatus)Enum.Parse(typeof(NodeStatus), status, true);
        }

        public List<Node> GetNodes()
        {
            return Nodes;
        }
    }
}
