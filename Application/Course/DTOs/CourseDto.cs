using Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace Backend.Application.Course.DTOs
{
    public record CreateCourseDto(
        [Required] string Name,
        [Required] string Description,
        [Required] double Price
    );

    public record CourseDto(
        string Name,
        string Description,
        double Price,
        DateTime CreateAt
    ) : CreateCourseDto(Name, Description, Price);
}
