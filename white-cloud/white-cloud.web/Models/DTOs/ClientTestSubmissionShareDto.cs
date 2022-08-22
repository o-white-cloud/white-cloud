using white_cloud.entities.Tests;

namespace white_cloud.web.Models.DTOs
{
    public class ClientTestSubmissionShareDto
    {
        public int Id { get; set; }
        public DateTime ShareDate { get; set; }
        public int TestSubmissionId { get; set; }
        public string TestSubmissionTestName { get; set; }
        public int TestSubmissionTestId { get; set; }
        public int TestSubmissionResultId { get; set; }
        public object TestSubmissionResultData { get; set; }
    }
}
