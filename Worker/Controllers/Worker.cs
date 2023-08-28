using Microsoft.AspNetCore.Mvc;
using Worker.Service;

namespace Worker.Controllers
{
    [ApiController]
    public class Worker : ControllerBase
    {
        private readonly FileDownloader _fileDownloader;
        private readonly IConfiguration _configuration;

        public Worker(FileDownloader fileDownloader, IConfiguration configuration)
        {
            _fileDownloader = fileDownloader;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("status")]
        public IActionResult Status()
        {
            return Ok();
        }

        [HttpGet]
        [Route("work")]
        public async Task<IActionResult> WorkAsync(string taskId)
        {
            _fileDownloader.DownloadFile(taskId,_configuration.GetValue<string>("DownloadUrl"), $"{taskId}.jpg");

            return Ok();
        }
    }
}
