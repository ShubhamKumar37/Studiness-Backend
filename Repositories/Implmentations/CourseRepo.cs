using Backend.Data;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories.Implmentations
{
    public class CourseRepo : ICourseRepo
    {
        private readonly AppDbContext _context;

        public CourseRepo(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<Course> CreateCourse(Course course)
        {
            await _context.Courses.AddAsync(course);
            return course;
        }

        public async Task<Course?> GetCourseById(int courseId)
        {
            return await _context.Courses.FirstOrDefaultAsync((x) => x.Id == courseId);
        }

        public async Task<List<Course?>> GetCourse(int page, int limit)
        {
            return await _context.Courses.OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToListAsync();
        }

    }
}
