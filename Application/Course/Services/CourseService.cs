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

        public CourseService(ICourseRepo courseRepo)
        {
            this._courseRepo = courseRepo;
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

        public async Task<List<BM.Course?>> GetCourse(int page, int limit)
        {
            return await _courseRepo.GetCourse(page, limit);
        }

        public async Task<BM.Course?> GetCourseById(int courseId)
        {
            BM.Course? courseExist = await _courseRepo.GetCourseById(courseId);


            return courseExist == null ? throw new NotFoundException("Course not found") : courseExist;
        }
        public Task Delete()
        {
            throw new NotImplementedException();
        }
    }
}
