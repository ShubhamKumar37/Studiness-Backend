using Backend.Models;

namespace Backend.Application.CourseSection.Interfaces
{
    public interface ICourseSectionService
    {
        Task<Section> AddCourseSection(int id, Section ncs, int userId);
        Task AddCourseSectionAttachment(int id, IFormFile file, int userId);
        Task<Section> GetCourseSection(int id);
        Task<List<Section>> GetCourseSections(int couresId, int page = 1, int limit = 10);
        Task<Section> UpdateCourseSection(Section section, int userId);
        Task DeleteCourseSection(int id, int userId);
    }
}
