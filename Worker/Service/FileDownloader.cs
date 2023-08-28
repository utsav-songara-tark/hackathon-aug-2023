using Newtonsoft.Json;
using System.Net;
using System.Text;
using Worker.Models;

namespace Worker.Service
{
    public class FileDownloader
    {
        private readonly ILogger<FileDownloader> _logger;
        private readonly IConfiguration _configuration;
        private readonly WorkerInfo _workerInfo;

        public FileDownloader(WorkerInfo workerInfo,ILogger<FileDownloader> logger, IConfiguration configuration)
        {
            _workerInfo = workerInfo;
            _logger = logger;
            _configuration = configuration;
        }

        public void DownloadFile(string taskId, string url, string fileName)
        {
            var webClient = new WebClient();
            var json = webClient.DownloadString(url);
            Mems mems = JsonConvert.DeserializeObject<Mems>(json);
            if (mems.nsfw)
            {
                UpdateTaskStatus(taskId, "Failed");
                return;
            }
            var filePath = Path.Combine(_workerInfo.WorkDir, fileName);
            if (!Directory.Exists(_workerInfo.WorkDir))
                Directory.CreateDirectory(_workerInfo.WorkDir);
            webClient.DownloadFile(new Uri(mems.url), filePath);
            _logger.LogInformation("File {FileName} downloaded to {FilePath}", fileName, filePath);
            UpdateTaskStatus(taskId, "Completed");
        }

        private void UpdateTaskStatus(string taskId, string status)
        {
            Models.TaskStatus taskStatus = new()
            {
                Status = status
            };

            _logger.LogInformation("Task status is {TaskStatus} {url}", status, _configuration.GetValue<string>("AllocatorUri"));

            var objAsJson = JsonConvert.SerializeObject(taskStatus);
            var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");
            new HttpClient().PutAsync($"{_configuration.GetValue<string>("AllocatorUri")}/api/task/status/{taskId}", content);
        }
    }
}
