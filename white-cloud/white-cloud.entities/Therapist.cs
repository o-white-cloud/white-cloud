using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace white_cloud.entities
{
    public class Therapist
    {
        public int Id { get; set; }
        public string CopsiNumber { get; set; } = String.Empty;
        public string UserId { get; set; }

        public User User { get; set; }
        public List<Client> Clients { get; set; }
        public List<ClientInvite> ClientInvites { get; set; }
    }
}
