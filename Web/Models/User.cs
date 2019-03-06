using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class User : IdentityUser<Guid>
    {
        public User() : base () { }
        public Role Role { get; set; }
        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
