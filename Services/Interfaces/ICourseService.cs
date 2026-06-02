using Backend.Dto;
using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface ICourseService
    {
        Task<Course> Create(CreateCourseDto ccd, int userId);
        Task<List<Course?>> GetCourse(int page, int limit);
        Task<Course?> GetCourseById(int id);
        Task Delete();
    }
}
