using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace SchedulingBusinessSoftware.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
        public ApplicationRole() { }
    }
}
