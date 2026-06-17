using Backend.Application.Course.DTOs;
using Backend.Application.Course.Interfaces;
using Backend.Exceptions;
using Backend.Models;
using Backend.Shared.Responses;
using Backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ICloudinaryUtils _cloudinaryUtil;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ICourseService courseService, ICloudinaryUtils cloudinaryUtil, ILogger<CourseController> logger)
        {
            this._courseService = courseService;
            this._cloudinaryUtil = cloudinaryUtil;
            this._logger = logger;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto ccd)
        {
            //_logger.LogInformation($"{User?.FindFirst(ClaimTypes.Role)?.Value}, {User?.FindFirst(ClaimTypes.NameIdentifier)?.Value}");
            if (!User.IsInRole("Instructor")) throw new ApiException(401, "You are not authorized to create course");
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier) ?? throw new ApiException(401, "User id not found in claims");

            await _courseService.Create(ccd, int.Parse(userId.Value));

            return Ok(ApiResponse<CreateCourseDto>.Ok(ccd, "Course created successfully"));
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            Course? course = await _courseService.GetCourseById(id);
            if (course == null) throw new NotFoundException("Course Not found");

            CourseDto cd = new CourseDto(
                course.Name,
                course.Description,
                course.Price,
                course.CreatedAt
            );
            return Ok(ApiResponse<CourseDto>.Ok(cd, "Course fetched"));
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetCourse([FromQuery] int page = 1, [FromQuery] int limit = 5)
        {
            List<Course?> listCourse = await _courseService.GetCourse(page, limit);

            return Ok(ApiResponse<List<Course?>>.Ok(listCourse, "Courses fetched"));
        }

        [HttpPut("update/{courseId}")]
        [Authorize]
        public async Task<IActionResult> UpdateCourse([FromRoute] int courseId, [FromBody] CreateCourseDto ccd)
        {
            if (!User.IsInRole("Instructor")) throw new ApiException(401, "You are not authorized to update course");
            int userId = int.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            await _courseService.Update(courseId, userId, ccd);
            return Ok(ApiResponse<CreateCourseDto>.Ok(ccd, "Course updated successfully"));
        }


        [HttpPut("upload")]
        [Authorize(Roles = "Instructor")]
        [RequestSizeLimit(100 * 1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = 100 * 1024 * 1024)]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            var res = await _cloudinaryUtil.UploadFile(file);

            return Ok(ApiResponse.Ok("File upload successfully"));
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> Delete([FromQuery] string publicId, [FromQuery] bool type)
        {
            bool flag = await _cloudinaryUtil.DeleteFile(publicId, type);
            if (flag) return Ok(ApiResponse.Ok("File deleted successfully"));
            return NoContent();

        }
    }
}
