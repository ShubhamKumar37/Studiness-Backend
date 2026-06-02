namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public Role Role { get; set; } = Role.Student;

        public ICollection<Course> PublishedCourse { get; set; } = new List<Course>();
    }

    public enum Role
    {
        Student, 
        Instructor, 
        Admin
    }
}
