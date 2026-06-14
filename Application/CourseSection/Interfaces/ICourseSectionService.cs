using Backend.Models;

namespace Backend.Application.CourseSection.Interfaces
{
    public interface ICourseSectionService
    {
        Task<Section> AddCourseSection(int id, Section ncs);
        Task AddCourseSectionAttachment(int id, IFormFile file);
        Task<Section> GetCourseSection(int id);
        Task<List<Section>> GetCourseSections(int couresId, int page = 1, int limit = 10);
        Task<Section> UpdateCourseSection(Section section);
        Task DeleteCourseSection(int id);
    }
}
