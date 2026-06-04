namespace Backend.Models
{
    public class Course : BaseEntity
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public double Price { get; set; } = 0;
        public int UserId { get; set; }
        public User Publisher { get; set; } = null!;

        public List<Section> Sections { get; set; } = new List<Section>();
    }
}
