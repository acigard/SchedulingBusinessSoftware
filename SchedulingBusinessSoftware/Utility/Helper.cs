using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchedulingBusinessSoftware.Utility
{
    public static class Helper
    {
        public static string Admin = "Admin";
        public static string Recruiter = "Recruiter";
        public static string Candidate = "Candidate";

        public static List<SelectListItem> GetRolesFromDropdow(bool isAdmin)
        {
            if (isAdmin)
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = Helper.Admin, Text = Admin },
                };
            }
            else
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = Helper.Recruiter },
                    new SelectListItem { Value = Helper.Candidate },
                };
            }
        }
    }
}

