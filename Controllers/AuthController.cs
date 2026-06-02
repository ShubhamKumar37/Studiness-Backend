using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Dto;
using Backend.Services.Interfaces;
using Backend.Utils;
using Backend.Settings;
using System.ComponentModel.DataAnnotations;
using Backend.Shared.Responses;


namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly SendMail _sendMail;

        public AuthController(IAuthService authservice)
        {
            this._authService = authservice;
        }


        [HttpPut("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            string token = await _authService.Login(user);

            Response.Cookies.Append("token", token, new CookieOptions {
                Expires = DateTime.UtcNow.AddDays(7),
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });
            return Ok(ApiResponse.Ok("Login Successfully"));
        }

        [HttpPost("otp")]
        public async Task<IActionResult> SendOtp([FromBody] EmailDto email)
        {
            await _authService.SendOtp(email);
            return Ok(ApiResponse.Ok("Otp Send Successfully"));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserCreateDto user)
        {
            await _authService.Create(user);
            return Ok(ApiResponse.Ok("New User Created"));
        }

        [HttpPut("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("token");
            return Ok(ApiResponse.Ok("Logout Sucessful"));
        }
    }

    
}
