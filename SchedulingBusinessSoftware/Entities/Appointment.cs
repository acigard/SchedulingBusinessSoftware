using System.ComponentModel.DataAnnotations;

namespace SchedulingBusinessSoftware.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public Type AppointmentType { get; set;
        public string Type { get; set; } = "HR";
        public string? Candidate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ScheduledAt { get; set; }   
        //public enum Type
        //{
        //    Technical,
        //    HR
        //}

    }

}
