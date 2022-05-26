namespace white_cloud.entities.Tests
{
    public class TestRequest
    {
        public int Id { get; set; }
        public int TherapistId { get; set; }
        public int ClientId { get; set; }
        public DateTime SentDate { get; set; }
        public int TestId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public bool SendEmailOnSubmission { get; set; }
        
        public Therapist Therapist { get; set; }
        public Client Client { get; set; }
        public TestSubmission? TestSubmission { get; set; }  
    }
}
