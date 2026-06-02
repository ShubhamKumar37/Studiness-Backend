using Backend.Models;

namespace Backend.Repositories.Interfaces
{
    public interface ICourseRepo
    {
        Task<Course?> CreateCourse(Course cs);
        Task<Course?> GetCourseById(int courseId);
        Task<List<Course?>> GetCourse(int page, int limit);
    }
}
