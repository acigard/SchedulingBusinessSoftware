using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchedulingBusinessSoftware.Data.DbContexts;
using SchedulingBusinessSoftware.Data.Seeder;
using SchedulingBusinessSoftware.Models;
//using SchedulingBusinessSoftware.Services;
using System.Diagnostics;

namespace SchedulingBusinessSoftware.Controllers
{
    public class HomeController : Controller

    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppointmentSeeader _appointmentSeeader;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppointmentSeeader appointmentSeeader, AppDbContext context) 
        {
            _logger = logger;
            _appointmentSeeader = appointmentSeeader;
            _context = context;
        }

        //the db will show the logged user by using httpContext
        public async Task <IActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            var result = await _appointmentSeeader.SeedData(userName);

            var userAppointment = _context.Appoitment.Where(a => a.Candidate == userName).Take(5).ToList();

            var viewModel = new IndexViewModel
            {
                UserAppointments = userAppointment
            };
           

            return View(viewModel);
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