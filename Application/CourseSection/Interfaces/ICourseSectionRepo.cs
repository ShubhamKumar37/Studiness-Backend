using Backend.Models;

namespace Backend.Application.CourseSection.Interfaces
{
    public interface ICourseSectionRepo
    {
        Task<Section> AddSection(Section section);
        Task<Section> GetSectionById(int id);
        Task<List<Section>> GetSections(int courseId);
        Task<Section> UpdateSection(Section section);
        Task DeleteSection(int id);
    }
}
