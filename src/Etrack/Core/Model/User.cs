using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace Etrack.Core.Model
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        public virtual ICollection<UserLocation> AssignedLocations { get; set; }

    }
}
