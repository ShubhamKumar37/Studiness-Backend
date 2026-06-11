using Backend.Models;

namespace Backend.Application.CourseSection.Interfaces
{
    public interface ICourseSectionRepo
    {
        Task AddSection(Section section);
        Task AddAttachment(Attachment attachment);
        Task<Section?> GetSectionById(int id);
        Task<List<Section>> GetSections(int courseId);
        Task UpdateSection(Section section);
        Task DeleteSection(Section section);
    }
}
