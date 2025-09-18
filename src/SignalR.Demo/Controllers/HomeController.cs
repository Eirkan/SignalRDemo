using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SignalR.Demo.Models;
using SignalR.Demo.SignalR;

namespace SignalR.Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProgressReporterFactory _progressReporterFactory;

        public HomeController(IProgressReporterFactory progressReporterFactory)
        {
            _progressReporterFactory = progressReporterFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Load(LoadViewModel loadViewModel, CancellationToken cancellationToken)
        {
            var progressReporter = _progressReporterFactory.GetLoadingBarReporter(loadViewModel.ConnectionId);

            for (int i = 0; i < loadViewModel.Seconds; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return NoContent();
                }

                progressReporter.Report(1 / (double)loadViewModel.Seconds);
                await Task.Delay(1000);
            }

            return Content("Completed");

        }
    }
}
