using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace white_cloud.entities
{
    public class Client
    {
        public int Id { get; set; }
        public int TherapistId { get; set; } = 0;
        public string UserId { get; set; } = "";
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string Ocupation { get; set; }
        /// <summary>
        /// Date when the client became associated with the Therapist
        /// </summary>
        public DateTime ClientDate { get; set; }
        public Therapist Therapist { get; set; }
        public User User { get; set; }
    }

    public enum Gender
    {
        Female = 1,
        Male = 2,
        Other = 3
    }

}
