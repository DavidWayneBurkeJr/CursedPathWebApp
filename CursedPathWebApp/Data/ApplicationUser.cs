using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace CursedPathWebApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Logins { get; } = new List<ApplicationUser>();
        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; } = new List<IdentityUserClaim<string>>();

    }
}