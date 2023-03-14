using System.ComponentModel.DataAnnotations;

namespace SchedulingBusinessSoftware.Models
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set;
        }
    }
}
