using Newtonsoft.Json;
using Organizations.Models.DTO;
using Organizations.Services.Interfaces;
using Quartz;

namespace Organizations.API.Jobs
{
    public class DailyTotalJob : IJob
    {
        private readonly IDailyTotalService _dailyTotalService;
        private readonly IConfiguration _configuration;
        public DailyTotalJob(IDailyTotalService dailyTotalService, IConfiguration configuration)
        {
            _dailyTotalService = dailyTotalService;
            _configuration = configuration;
        }
        public Task Execute(IJobExecutionContext ctx)
        {
            DailyReport dailyReport = _dailyTotalService.GetDailyReport();
            string content = JsonConvert.SerializeObject(dailyReport);
            File.WriteAllText(GeneratePath(dailyReport), content);
            return Task.CompletedTask;
        }

        private string GeneratePath(DailyReport dailyReport)
        {
            string folder = _configuration.GetSection("ReportFolder").Value;
            DateTime date = dailyReport.Date;
            string fileName = date.Year.ToString() + date.Month.ToString() + date.Day.ToString() + ".json";
            string path = Path.Combine(folder, fileName);
            return path;
        }

    }
}
