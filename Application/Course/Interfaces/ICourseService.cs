using Backend.Application.Course.DTOs;
using Backend.Models;

namespace Backend.Application.Course.Interfaces
{
    public interface ICourseService
    {
        Task<Models.Course> Create(CreateCourseDto ccd, int userId);
        Task<List<Models.Course>> GetCourse(int page, int limit);
        Task<Models.Course> GetCourseById(int id);
        Task Update(int courseId, int courseOwnerId, CreateCourseDto ccd);
        
    }
}
