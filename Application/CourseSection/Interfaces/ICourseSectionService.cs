using Backend.Models;

namespace Backend.Application.CourseSection.Interfaces
{
    public interface ICourseSectionService
    {
        Task<Section> AddCourseSection(int id, Section ncs);
        Task<Section> GetCourseSection(int id);
        Task<List<Section>> GetCourseSections(int couresId);
    }
}
