using Backend.Models;

namespace Backend.Application.CourseSection.Interfaces
{
    public interface ICourseSectionRepo
    {
        Task AddSection(Section section);
        Task AddAttachment(Attachment attachment);
        Task<Section?> GetSectionById(int id);
        Task<List<Section>> GetSections(int courseId, int page = 1, int limit = 10);
        Task DeleteSection(Section section);
        Task SaveContext();
    }
}
