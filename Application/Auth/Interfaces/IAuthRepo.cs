using Backend.Application.Auth.DTOs;
using Backend.Models;
namespace Backend.Application.Auth.Interfaces
{
    public interface IAuthRepo
    {
        Task<User?> GetUserById(int Id);
        Task<User?> GetUserByEmail(string email);
        Task AddOtp(string otp, string email);
        Task<Otp?> GetOtp(string email);
        Task<User?> CreateUser(UserCreateDto ucd);
        Task SaveAsync();
    }
}
