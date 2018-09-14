using Anotar.Serilog;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SerilogSQLTest.Models;
using System.Diagnostics;

namespace SerilogSQLTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Log.Warning("Division by zero");
            Log.Error("Division by zero");
            Log.Information("Division by zero");
            LogTo.Warning("Division by zero");
            LogTo.Error("Division by zero");
            LogTo.Information("Division by zero");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
    }
}
