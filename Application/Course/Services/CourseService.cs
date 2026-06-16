using Backend.Application.Course.DTOs;
using Backend.Application.Course.Interfaces;
using Backend.Exceptions;
using BM = Backend.Models;
using CloudinaryDotNet;

namespace Backend.Application.Course.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepo _courseRepo;
        private readonly ILogger<CourseService> _iLogger;

        public CourseService(ICourseRepo courseRepo, ILogger<CourseService> iLogger)
        {
            this._courseRepo = courseRepo;
            this._iLogger = iLogger;
        }
        public async Task<BM.Course> Create(CreateCourseDto ccd, int userId)
        {
            var newCourse = new BM.Course()
            {
                Name = ccd.Name,
                Description = ccd.Description,
                Price = ccd.Price,
                UserId = userId
            };
            await _courseRepo.CreateCourse(newCourse);

            return newCourse;
        }

        public async Task<List<BM.Course>> GetCourse(int page, int limit)
        {
            return await _courseRepo.GetCourse(page, limit);
        }

        public async Task<BM.Course?> GetCourseById(int courseId)
        {
            BM.Course? courseExist = await _courseRepo.GetCourseById(courseId) ?? throw new ApiException(404, "Course not found");
            return courseExist;
        }
        public async Task Update(int courseId, int courseOwnerId, CreateCourseDto ccd)
        {
            _iLogger.LogInformation($"This is the course id - {courseId} and the owner id is - {courseOwnerId}");
            BM.Course? courseExist = await _courseRepo.GetCourseById(courseId) ?? throw new ApiException(404, "Course not found");

            if (courseExist.UserId != courseOwnerId) throw new ApiException(401, "You are not the owner of course");

            courseExist.Name = ccd.Name;
            courseExist.Description = ccd.Description;
            courseExist.Price = ccd.Price;

            await _courseRepo.SaveChangeAsync();
        }
    }
}
