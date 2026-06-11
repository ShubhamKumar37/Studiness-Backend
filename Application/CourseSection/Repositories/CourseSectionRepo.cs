using Backend.Application.CourseSection.Interfaces;
using Backend.Data;
using Backend.Models;

namespace Backend.Application.CourseSection.Repositories
{
    public class CourseSectionRepo : ICourseSectionRepo
    {
        private readonly AppDbContext _context;

        public CourseSectionRepo(AppDbContext context)
        {
            this._context = context;    
        }
        public async Task AddAttachment(Attachment attachment)
        {
            await _context.Attachments.AddAsync(attachment);
            await SaveContext();
        }

        public async Task AddSection(Section section)
        {
            await _context.Sections.AddAsync(section);
            await SaveContext();
        }

        public async Task DeleteSection(Section section)
        {
            _context.Sections.Remove(section);
            await SaveContext();

        }

        public async Task<Section?> GetSectionById(int id)
        {
            Section? section = await _context.Sections.FindAsync(id);
            return section;
        }

        public Task<List<Section>> GetSections(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSection(Section section)
        {
            throw new NotImplementedException();
        }

        public async Task SaveContext()
        {
            await _context.SaveChangesAsync();
        }
    }
}
