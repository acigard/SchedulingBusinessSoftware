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

        public async Task<IActionResult> AddOrEdit(int? Id)
        {
            ViewBag.PageName = Id == null ? "Create Appointment" : "Edit Appointment";
            ViewBag.isEdit = Id == null ? false : true;
            if (Id == null)
            {
                return View();
            }
            else
            {
                var appointment = await _context.Appoitment.FindAsync(Id);
                if (appointment == null)
                {
                    return NotFound();
                }
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int Id, [Bind("Id, Title,Description,CreatedDate,UpdateDate,ScheduledAt")] Appointment appointment)
        {

            bool isAppointmentExist = false;

            Appointment app = await _context.Appoitment.FindAsync(Id);
            AppointmentViewModel model = _mapper.Map<AppointmentViewModel>(app);

            if (app != null)
            {
                isAppointmentExist = true;
            }
            else
            {
                app = new Appointment();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    app.Id = Id;
                    app.Title = appointment.Title;
                    app.Description = appointment.Description;
                    app.ScheduledAt = DateTime.Now;
                    app.CreatedDate = DateTime.Now;
                    app.UpdatedDate = DateTime.Now;

                    if (isAppointmentExist)
                    {
                        _context.Update(app);
                    }
                    else
                    {
                        _context.Add(app);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int Id)
        {
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
