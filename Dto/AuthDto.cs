using Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace Backend.Dto
{
    public enum Role
    {
        Student,
        Instructor,
        Admin
    }
    public class UserCreateDto
    {
        [Required]
        public string Name { get; set; } = "";
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        [MinLength(3)]
        public string Password { get; set; } = "";
        public string Otp { get; set; } 
        public Role Role { get; set; } = Role.Student;

    }

    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        [MinLength(3)]
        public string Password { get; set; } = "";
    }

    public class UserLoginReturnDto
    {
        public string Token { get; set; } = "";
    }

    public class SendOtpDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        public int Otp { get; set; }
    }

    public class EmailDto
    {
        [Required]
        public string Email { get; set; }
    }
}
