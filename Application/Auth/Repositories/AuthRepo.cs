using Backend.Application.Auth.DTOs;
using Backend.Application.Auth.Interfaces;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Auth.Repositories
{
    public class AuthRepo : IAuthRepo
    {
        private readonly AppDbContext _context;

        public AuthRepo(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddOtp(Otp newOtp)
        {
            await _context.Otps.AddAsync(newOtp);
            await SaveAsync();
        }
        public async Task<Otp?> GetOtp(string email)
        {
            return await _context.Otps.Where(x => x.Email == email).OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        }
        public async Task<User?> CreateUser(UserCreateDto ucd)
        {
            User user = new User()
            {
                Name = ucd.Name,
                Email = ucd.Email,
                Password = ucd.Password,
                Role = ucd.Role
            };
            await _context.Users.AddAsync(user);
            await SaveAsync();
            return user;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
