using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace white_cloud.identity.Entities
{
    public class User : IdentityUser
    {
        public int? TherapistId { get; set; }
    }
}
