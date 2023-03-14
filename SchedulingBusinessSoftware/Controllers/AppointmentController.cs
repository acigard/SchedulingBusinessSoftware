using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchedulingBusinessSoftware.Data.DbContexts;
using SchedulingBusinessSoftware.Entities;
using SchedulingBusinessSoftware.Models;
using System.Reflection;

namespace SchedulingBusinessSoftware.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        
        public AppointmentController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var appointment = await _context.Appoitment.ToListAsync();
            List<AppointmentViewModel> appointmentViewModels = _mapper.Map<List<AppointmentViewModel>>(appointment);
            return View(appointmentViewModels);
            //var result = await _context.Appoitment.ToListAsync();
            //return View(result);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _context.Appoitment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<AppointmentViewModel>(appointment);

            return View(model);
        }

        public async Task<IActionResult> Details(int Id)
        {

            List<Interviewer> interviewers = new List<Interviewer>()
            {
                new Interviewer()
                {
                    Id = Id,
                    FistName = "FName",
                    LastName = "LName",
                    Position = "Recruiter"
                },
            };

            if (Id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appoitment.FirstOrDefaultAsync(a => a.Id == Id);
            AppointmentViewModel model = _mapper.Map<AppointmentViewModel>(appointment);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        //GET Appointment for method Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var app = await _context.Appoitment.FirstOrDefaultAsync(a => a.Id == id);
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var appointment = await _context.Appoitment.FindAsync(Id);
            _context.Appoitment.Remove(appointment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
