using Microsoft.EntityFrameworkCore;
using Backend.Models;
namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users{ get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Otp> Otps { get; set; }

    }
}
