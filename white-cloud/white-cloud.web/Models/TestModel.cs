namespace white_cloud.web.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Icon { get; set; } = "test";
        public string Excerpt { get; set; } = "";
        public string Description { get; set; } = "";
        public List<object> Questions { get; set; } = new List<object>();
    }
}
