using Backend.Application.Course.Interfaces;
using Backend.Application.CourseSection.DTOs;
using Backend.Application.CourseSection.Interfaces;
using Backend.Exceptions;
using Backend.Models;
using bm = Backend.Models;

namespace Backend.Application.CourseSection.Services
{
    public class CourseSectionService : ICourseSectionService
    {
        private readonly ICourseSectionRepo _courseSectionRepo;
        private readonly ICourseRepo _courseRepo;

        public CourseSectionService(ICourseSectionRepo courseSectionRepo, ICourseRepo courseRepo)
        {
            this._courseSectionRepo = courseSectionRepo;
            this._courseRepo = courseRepo;
        }

        public async Task<bm.Section> AddCourseSection(int id, bm.Section ncs)
        {
            bm.Course courseExist = await _courseRepo.GetCourseById(id) ?? throw new ApiException(404, "Course does not exist");
            bm.Section newSection = await _courseSectionRepo.AddSection(ncs);
            return newSection;
        }

        public async Task<bm.Section> GetCourseSection(int id)
        {
            bm.Section section = await _courseSectionRepo.GetSectionById(id) ?? throw new ApiException(404, "Section does not exist");

            return section;
        }

        public async Task<List<bm.Section>> GetCourseSections(int courseId)
        {
            List<bm.Section> sectionList = await _courseSectionRepo.GetSections(courseId);

            return sectionList;
        }

        public async Task<bm.Section> UpdateCourseSection(Section section)
        {
            bm.Section sectionExist = await _courseSectionRepo.GetSectionById(section.Id) ?? throw new ApiException(404, "Section does not exist");
            bm.Section updatedSection = await _courseSectionRepo.UpdateSection(section);

            return updatedSection;
        }

        public async Task DeleteCourseSection(int id)
        {
            await _courseSectionRepo.DeleteSection(id);
        }

    }
}
