using Backend.Application.Auth.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
namespace Backend.Application.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<User?> Create(UserCreateDto ucd);
        Task<string> Login(UserLoginDto uld);
        Task SendOtp(EmailDto email);
    }
}
