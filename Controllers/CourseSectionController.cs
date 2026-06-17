using Backend.Application.Course.Interfaces;
using Backend.Application.CourseSection.DTOs;
using Backend.Application.CourseSection.Interfaces;
using Backend.Exceptions;
using Backend.Models;
using Backend.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseSectionController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ICourseSectionService _courseSectionService;

        public CourseSectionController(ICourseService courseService, ICourseSectionService courseSectionService)
        {
            this._courseService = courseService;
            this._courseSectionService = courseSectionService;
        }


        [HttpPost("add-section/{courseId}")]
        [Authorize]
        public async Task<IActionResult> AddSection([FromRoute] int courseId, NewCourseSection ncs)
        {
            if (!User.IsInRole("Instructor")) throw new ApiException(401, "You are not authorized to update course");
            int userId = int.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            Section tempSection = new()
            {
                Name = ncs.Name,
                CourseId = ncs.CourseId
            };

            Section newSection = await _courseSectionService.AddCourseSection(courseId, tempSection, userId);
            CourseSectionDto response = new(newSection.Id, newSection.Name, courseId);

            return Ok(ApiResponse<CourseSectionDto>.Ok(response,"New Section Added"));
        }

        [HttpPost("add-section-attachment/{id}")]
        [Authorize]
        public async Task<IActionResult> AddSectionAttachment([FromRoute] int id, [FromForm] IFormFile file)
        {
            if (!User.IsInRole("Instructor")) throw new ApiException(401, "You are not authorized to update course");
            int userId = int.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            await _courseSectionService.AddCourseSectionAttachment(id, file, userId);

            return Ok(ApiResponse.Ok("Attachment added successfully"));
        }

        [HttpGet("get/{courseId}")]
        public async Task<IActionResult> GetSections([FromRoute] int courseId, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            List<Section> sections = await _courseSectionService.GetCourseSections(courseId, page, limit);
            List<CourseSectionDto> allSections = new();

            foreach(var i in sections) allSections.Add(new(i.Id, i.Name, i.CourseId));

            return Ok(ApiResponse<List<CourseSectionDto>>.Ok(allSections, "All section fetched"));
            
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSection([FromRoute] int id, [FromBody] NewCourseSection ncs)
        {
            if (!User.IsInRole("Instructor")) throw new ApiException(401, "You are not authorized to update course");
            int userId = int.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            Section tempSection = new()
            {
                Id = id,
                Name = ncs.Name,
                CourseId = ncs.CourseId
            };

            Section updatedSection = await _courseSectionService.UpdateCourseSection(tempSection, userId);
            CourseSectionDto response = new(updatedSection.Id, updatedSection.Name, updatedSection.CourseId);

            return Ok(ApiResponse<CourseSectionDto>.Ok(response, "Section updated successfully"));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSection([FromRoute] int id)
        {
            if (!User.IsInRole("Instructor")) throw new ApiException(401, "You are not authorized to update course");
            int userId = int.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            await _courseSectionService.DeleteCourseSection(id, userId);
            return Ok(ApiResponse.Ok("Section deleted successfully"));
        }
    }
}
