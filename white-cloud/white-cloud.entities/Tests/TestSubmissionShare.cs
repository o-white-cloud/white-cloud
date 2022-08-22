using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace white_cloud.entities.Tests
{
    public class TestSubmissionShare
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TherapistId { get; set; }

        public int TestSubmissionId { get; set; } = 0;
        public DateTime ShareDate { get; set; }

        public TestSubmission TestSubmission { get; set; }
        public Therapist Therapist { get; set; }
    }
}
