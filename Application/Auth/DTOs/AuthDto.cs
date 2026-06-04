using Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace Backend.Application.Auth.DTOs
{
    public enum Role
    {
        Student,
        Instructor,
        Admin
    }
    public record UserCreateDto
    (
        [Required]
        string Name,
        
        [Required]
        [EmailAddress]
        string Email,
        
        [Required]
        [MinLength(3)]
        string Password,
        
        string Otp, 
        
        Role Role = Role.Student
    );

    public record UserLoginDto(
        [Required]
        [EmailAddress]
        string Email,

        [Required]
        [MinLength(3)]
        string Password
    );

    public record UserLoginReturnDto(
        string Token
    );

    public record SendOtpDto(
        [Required]
        [EmailAddress]
        string Email,

        int Otp
    );

    public record EmailDto(
        [Required]
        string Email
    );
}
