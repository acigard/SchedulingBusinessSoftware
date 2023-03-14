using SchedulingBusinessSoftware.Entities;

namespace SchedulingBusinessSoftware.Models
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set;}
        public string Type { get; set; }
        public DateTime ScheduledAt { get; set; }

        public Interviewer Interviewer { get; set; }
    }
}
