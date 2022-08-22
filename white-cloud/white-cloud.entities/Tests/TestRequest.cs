namespace white_cloud.entities.Tests
{
    public class TestRequest
    {
        public int Id { get; set; }
        public int TherapistId { get; set; }
        public int ClientId { get; set; }
        public DateTime SentDate { get; set; }
        public int TestId { get; set; }
        public int? TestSubmissionShareId { get; set; }

        public Therapist Therapist { get; set; }
        public Client Client { get; set; }
        public TestSubmissionShare? TestSubmissionShare { get; set; }
    }
}
