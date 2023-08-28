using Microsoft.AspNetCore.Mvc;
using TaskExecutor.Models;
using TaskExecutor.Services;

namespace TaskExecutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodesController : ControllerBase
    {
        private readonly WorkerService _workerService;

        public NodesController(WorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterNode([FromBody] NodeRegistrationRequest node)
        {
            _workerService.RegisterNode(node);
            return Ok();
        }
        
        [HttpDelete]
        [Route("unregister/{name}")]
        public IActionResult RegisterNode(string name)
        {
            _workerService.UnregisterNode(name);
            return Ok();
        }

        [HttpGet]
        [Route("status")]
        public IActionResult GetAvailableNodes()
        {
            return Ok(_workerService.GetNodes());
        }
    }
}
