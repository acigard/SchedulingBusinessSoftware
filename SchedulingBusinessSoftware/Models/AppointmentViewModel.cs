namespace SchedulingBusinessSoftware.Models
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set;}

        public DateTime ScheduledAt { get; set; }
    }
}
