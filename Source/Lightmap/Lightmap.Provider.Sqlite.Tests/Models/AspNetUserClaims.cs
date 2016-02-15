using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Core.Tests.Models
{
    public class AspNetUserClaims
    {
        public int Id { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public string UserId { get; set; }
    }
}
