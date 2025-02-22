using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Core.Entities
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }

        //For Company
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
        public List<AppUserJob> UserJobs { get; set; }


        //For Member
        public string? CvUrl { get; set; }
    }
}
