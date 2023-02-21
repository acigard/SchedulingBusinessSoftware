using SchedulingBusinessSoftware.Entities;

namespace SchedulingBusinessSoftware.Models
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public string EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }

        public string? RoleName { get; set; }    
        public ApplicationRole? ApplicationRole { get; set; }
    }
}
