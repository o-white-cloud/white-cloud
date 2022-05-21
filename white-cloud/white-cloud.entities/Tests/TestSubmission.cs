using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace white_cloud.entities.Tests
{
    public class TestSubmission
    {
        public int TestId { get; set; }
        public Dictionary<int, string> Answers { get; set; } = new Dictionary<int, string>();
        public TestSubmissionResult Result { get; set; } = new TestSubmissionResult();
        public DateTime Timestamp { get; set; }
        public string Email { get; set; } = "";
    }
}
