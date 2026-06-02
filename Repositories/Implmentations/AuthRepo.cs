using Backend.Data;
using Backend.Dto;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories.Implmentations
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

        public async Task AddOtp(string otp, string email)
        {
            await _context.Otps.AddAsync(new Otp() { Value = otp, Email = email });
        }
        public async Task<Otp?> GetOtp(string email)
        {
            return await _context.Otps.FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<User?> CreateUser(UserCreateDto ucd)
        {
            User user = new User()
            {
                Name = ucd.Name,
                Email = ucd.Email,
                Password = ucd.Password
            };
            await _context.Users.AddAsync(user);

            return user;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
