using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Car.DLL.Entities
{
    public class AppUser : IdentityUser
    {
        public bool IsUserActive { get; set; } = true;
    }
}
