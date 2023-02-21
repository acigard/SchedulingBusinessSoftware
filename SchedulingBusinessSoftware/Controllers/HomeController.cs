using Microsoft.AspNetCore.Mvc;
using SchedulingBusinessSoftware.Models;
//using SchedulingBusinessSoftware.Services;
using System.Diagnostics;

namespace SchedulingBusinessSoftware.Controllers
{
    public class HomeController : Controller

    {
        private readonly ILogger<HomeController> _logger;
       // private readonly AccountService _accountService;

        public HomeController(ILogger<HomeController> logger) // AccountService accountService)
        {
            _logger = logger;
           // _accountService = accountService;
        }
       
        /*public IActionResult Index()
        {
            var x = _accountService.SignUp(new Account { UserName = "test", Password = "test123" });

            return View();
        }*/

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
    }
}