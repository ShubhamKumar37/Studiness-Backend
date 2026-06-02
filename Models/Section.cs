namespace Backend.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
