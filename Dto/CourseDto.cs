using Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace Backend.Dto
{
    public class CreateCourseDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
    }

    public class CourseDto : CreateCourseDto
    {
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public string Message { get; set; } = "";
    }
}
