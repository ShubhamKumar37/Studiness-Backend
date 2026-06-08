using Backend.Application.Course.Interfaces;
using Backend.Application.CourseSection.DTOs;
using Backend.Models;
using Backend.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseSectionController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseSectionController(ICourseService courseService)
        {
            this._courseService = courseService;
        }


        [HttpPost("add-section/{id}")]
        public IActionResult AddSection([FromQuery] int id, NewCourseSection ncs)
        {

            return Ok(ApiResponse.Ok("New Section Added"));
        }
        [HttpGet("/{id}")]
        public IActionResult GetSections([FromQuery] int id)
        {
            //return Ok(ApiResponse.Ok<List<Section>>("All Section fetched", ));
            return Ok(id);
        }

        [HttpPut("/{id}")]
        public IActionResult UpdateSection([FromQuery] int id, [FromBody] NewCourseSection ncs)
        {
            return Ok();
        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeleteSection([FromQuery] int id)
        {
            
            return Ok();
        }
    }
}
