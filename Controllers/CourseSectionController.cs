using Backend.Application.Course.Interfaces;
using Backend.Application.CourseSection.DTOs;
using Backend.Application.CourseSection.Interfaces;
using Backend.Models;
using Backend.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddSection([FromQuery] int courseId, NewCourseSection ncs)
        {
            Section tempSection = new()
            {
                Name = ncs.Name,
                CourseId = ncs.CourseId
            };

            Section newSection = await _courseSectionService.AddCourseSection(courseId, tempSection);
            CourseSectionDto response = new(newSection.Id, newSection.Name, courseId);

            return Ok(ApiResponse<CourseSectionDto>.Ok(response,"New Section Added"));
        }

        [HttpPost("add-section-attachment/{id}")]
        public async Task<IActionResult> AddSectionAttachment([FromQuery] int id, [FromForm] IFormFile file)
        {
            await _courseSectionService.AddCourseSectionAttachment(id, file);

            return Ok(ApiResponse.Ok("Attachment added successfully"));
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetSections([FromQuery] int courseId)
        {
            List<Section> sections = await _courseSectionService.GetCourseSections(courseId);
            List<CourseSectionDto> allSections = new();

            foreach(var i in sections) allSections.Add(new(i.Id, i.Name, i.CourseId));

            return Ok(ApiResponse<List<CourseSectionDto>>.Ok(allSections, "All section fetched"));
            
        }

        [HttpPut("/{id}")]
        public async Task<IActionResult> UpdateSection([FromQuery] int id, [FromBody] NewCourseSection ncs)
        {
            Section tempSection = new()
            {
                Id = id,
                Name = ncs.Name,
                CourseId = ncs.CourseId
            };

            Section updatedSection = await _courseSectionService.UpdateCourseSection(tempSection);
            CourseSectionDto response = new(updatedSection.Id, updatedSection.Name, updatedSection.CourseId);

            return Ok(ApiResponse<CourseSectionDto>.Ok(response, "Section updated successfully"));
        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeleteSection([FromQuery] int id)
        {
            await _courseSectionService.DeleteCourseSection(id);
            return Ok(ApiResponse.Ok("Section deleted successfully"));
        }
    }
}
