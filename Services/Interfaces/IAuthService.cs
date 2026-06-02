using Backend.Dto;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
namespace Backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User?> Create(UserCreateDto ucd);
        Task<string> Login(UserLoginDto uld);
        Task SendOtp(EmailDto email);
    }
}
