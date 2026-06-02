namespace Backend.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string Url { get; set; } = "";
        
        public int SectionId { get; set; }
        public Section Section { get; set; } = null!;
    }
}
