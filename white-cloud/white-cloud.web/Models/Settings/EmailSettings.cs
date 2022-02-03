namespace white_cloud.web.Models.Settings
{
    public class EmailSettings
    {
        public string From { get; set; } = "";
        public string Server { get; set; } = "";
        public int Port { get; set; }
        public string User { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
