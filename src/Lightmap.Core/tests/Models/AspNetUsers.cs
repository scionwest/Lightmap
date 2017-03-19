using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Models
{
    public class AspNetUsers
    {
        public string Id { get; set; }

        public int AccessFailedCount { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string Email { get; set; }

        public int EmailConfirmed { get; set; }

        public int LockoutEnabled { get; set; }

        public string LockoutEnd { get; set; }

        public string NormalizedEmail { get; set; }

        public string NormalizedUserName { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public int PhoneNumberConfirmed { get; set; }

        public string SecurityStamp { get; set; }

        public int TwoFactorEnabled { get; set; }

        public string UserName { get; set; }
    }
}
