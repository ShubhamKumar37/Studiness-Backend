using Backend.Application.Course.Interfaces;
using Backend.Data;
using BM = Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Course.Repositories
{
    public class CourseRepo : ICourseRepo
    {
        private readonly AppDbContext _context;

        public CourseRepo(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<BM.Course?> CreateCourse(BM.Course course)
        {
            await _context.Courses.AddAsync(course);
            await SaveChangeAsync();
            return course;
        }

        public async Task<BM.Course?> GetCourseById(int courseId)
        {
            return await _context.Courses.FirstOrDefaultAsync((x) => x.Id == courseId);
        }

        public async Task<List<BM.Course>?> GetCourse(int page, int limit)
        {
            return await _context.Courses.OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToListAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
