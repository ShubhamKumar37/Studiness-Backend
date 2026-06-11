namespace Backend.Application.CourseSection.DTOs
{
    public record NewCourseSection(
        string Name,
        int CourseId
    );

    public record CourseSectionDto(
        int Id,
        string Name,
        int CourseId
    );
    public record UploadResultDto(
        string Url,
        string PublicId
    );

}
