namespace Backend.Application.Course.Interfaces
{
    public interface ICourseRepo
    {
        
        Task<Models.Course?> CreateCourse(Models.Course cs);
        Task<Models.Course?> GetCourseById(int courseId);
        Task<List<Models.Course?>?> GetCourse(int page, int limit);
        Task SaveChangeAsync();
    }
}
