using Backend.Application.Course.Interfaces;
using Backend.Application.CourseSection.DTOs;
using Backend.Application.CourseSection.Interfaces;
using Backend.Exceptions;
using Backend.Models;
using Backend.Utils;
using static System.Collections.Specialized.BitVector32;
using bm = Backend.Models;

namespace Backend.Application.CourseSection.Services
{
    public class CourseSectionService : ICourseSectionService
    {
        private readonly ICourseSectionRepo _courseSectionRepo;
        private readonly ICourseRepo _courseRepo;
        private readonly ICloudinaryUtils _cloudinary;
        private readonly ILogger<CourseSectionService> _iLogger;

        public CourseSectionService(ICourseSectionRepo courseSectionRepo, ICourseRepo courseRepo, ICloudinaryUtils cloudinary, ILogger<CourseSectionService> iLogger)
        {
            this._courseSectionRepo = courseSectionRepo;
            this._courseRepo = courseRepo;
            this._cloudinary = cloudinary;
            this._iLogger = iLogger;
        }

        public async Task<bm.Section> AddCourseSection(int id, bm.Section ncs, int userId)
        {
            bm.Course courseExist = await _courseRepo.GetCourseById(id) ?? throw new ApiException(404, "Course does not exist");
            _iLogger.LogInformation($"userId  = {userId} and course user Id = {courseExist.UserId}");
            if (courseExist.UserId != userId) throw new ApiException(401, "You are not the owner of course");

            await _courseSectionRepo.AddSection(ncs);
            return ncs;
        }

        public async Task AddCourseSectionAttachment(int id, IFormFile file, int userId)
        {
            UploadResultDto urd = await _cloudinary.UploadFile(file);
            Attachment newAttachment = new()
            {
                SectionId = id,
                Url = urd.Url,
                PublicId = urd.PublicId
               
            };
            await _courseSectionRepo.AddAttachment(newAttachment);
        }

        public async Task<bm.Section> GetCourseSection(int id)
        {
            bm.Section section = await _courseSectionRepo.GetSectionById(id) ?? throw new ApiException(404, "Section does not exist");

            return section;
        }

        public async Task<List<bm.Section>> GetCourseSections(int courseId, int page = 1, int limit = 10)
        {
            List<bm.Section> sectionList = await _courseSectionRepo.GetSections(courseId, page, limit);

            return sectionList;
        }

        public async Task<bm.Section> UpdateCourseSection(bm.Section section, int userId)
        {
            bm.Section sectionExist = await _courseSectionRepo.GetSectionById(section.Id) ?? throw new ApiException(404, "Section does not exist");
            bm.Course? courseExist = await _courseRepo.GetCourseById(section.CourseId) ?? throw new ApiException(404, "Course does not exist");

            if (courseExist.UserId != userId) throw new ApiException(401, "You are not the owner of course");

            sectionExist.Name = section.Name;
            await _courseSectionRepo.SaveContext();

            return section;
        }

        public async Task DeleteCourseSection(int id, int userId)
        {
            bm.Section sectionExist = await _courseSectionRepo.GetSectionById(id) ?? throw new ApiException(404, "Section does not exist");
            bm.Course? courseExist = await _courseRepo.GetCourseById(sectionExist.CourseId) ?? throw new ApiException(404, "Course does not exist");

            if (courseExist.UserId != userId) throw new ApiException(401, "You are not the owner of course");

            await _courseSectionRepo.DeleteSection(sectionExist);
        }

    }
}
