using Backend.Dto;
using Backend.Exceptions;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using CloudinaryDotNet;

namespace Backend.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepo _courseRepo;

        public CourseService(ICourseRepo courseRepo)
        {
            this._courseRepo = courseRepo;
        }
        public async Task<Course> Create(CreateCourseDto ccd, int userId)
        {
            Course newCourse = new()
            {
                Name = ccd.Name,
                Description = ccd.Description,
                Price = ccd.Price,
                UserId = userId
            };
            await _courseRepo.CreateCourse(newCourse);

            return newCourse;
        }

        public async Task<List<Course?>> GetCourse(int page, int limit)
        {
            return await _courseRepo.GetCourse(page, limit);
        }

        public async Task<Course?> GetCourseById(int courseId)
        {
            Course? courseExist = await _courseRepo.GetCourseById(courseId);


            return courseExist == null ? throw new NotFoundException("Course not found") : courseExist;
        }
        public Task Delete()
        {
            throw new NotImplementedException();
        }
    }
}
