namespace Backend.Models
{
    public class Section : BaseEntity
    {
        public string Name { get; set; } = "";
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
