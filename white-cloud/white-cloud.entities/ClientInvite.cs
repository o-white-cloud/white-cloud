using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace white_cloud.entities
{
    public class ClientInvite
    {
        public int Id { get; set; }
        public int TherapistId { get; set; }
        public DateTime SentDate { get; set; }
        public string Email { get; set;}
        public DateTime? AcceptedDate { get; set; }
        
        public Therapist Therapist { get; set; }
    }
}
