using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Model
{
    /// <ApplicationUser>
    /// In this class we place the values that will have a user
    /// </ApplicationUser>
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string DocumentNumber { get; set; }

    }
}
