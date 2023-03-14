using SchedulingBusinessSoftware.Data.DbContexts;
using SchedulingBusinessSoftware.Entities;

namespace SchedulingBusinessSoftware.Data.Seeder
{
    public class AppointmentSeeader
    {
        private readonly AppDbContext _context;
        public AppointmentSeeader(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SeedData(string? userName)
        {

            List<Appointment> appointment = new List<Appointment>()
            {
                new Appointment()
                {
                     Title = "Appointment1",
                     Description = "Description Appointment",
                     CreatedDate = DateTime.Now,
                     UpdatedDate = DateTime.Now,
                     ScheduledAt = DateTime.Now.AddMonths(3),
                     Candidate = userName
                },
                new Appointment()
                {
                     Title = "Appointment2",
                     Description = "Description Appointment2",
                     CreatedDate = DateTime.Now,
                     UpdatedDate = DateTime.Now,
                     ScheduledAt = DateTime.Now.AddMonths(3),
                     Candidate = userName
                },
                  new Appointment()
                {
                     Title = "Appointment3",
                     Description = "Description Appointment",
                     CreatedDate = DateTime.Now,
                     UpdatedDate = DateTime.Now,
                     ScheduledAt = DateTime.Now.AddMonths(3),
                     Candidate = userName
                },
            };

            foreach (var Item in appointment)
            {
                await _context.Appoitment.AddAsync(Item);
            }

            await _context.SaveChangesAsync();
            return true;

        }
    }
}
