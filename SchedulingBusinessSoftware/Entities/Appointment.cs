namespace SchedulingBusinessSoftware.Entities
{
    public class Appointment
    {
      public int Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }

      public DateTime CreatedDate { get; set; }
      public DateTime UpdatedDate { get; set;}
      public DateTime ScheduledAt { get; set;}
    }
    
}
