namespace Backend.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public double Price { get; set; } = 0;
        public int UserId { get; set; }
        public User Publisher { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Section> Sections { get; set; } = new List<Section>();
    }
}
