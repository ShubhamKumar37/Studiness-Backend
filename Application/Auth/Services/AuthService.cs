using Backend.Models;
using Backend.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Exceptions;
using Backend.Application.Auth.Interfaces;
using Backend.Application.Auth.DTOs;

namespace Backend.Application.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _iAuthRepo;
        private readonly IConfiguration _configuration;
        private readonly SendMail _sendMail;
        private readonly ILogger _iLogger;

        
        public AuthService(IAuthRepo iAuthRepo, SendMail sm, ILogger<IAuthService> logger, IConfiguration configuration)
        {
            this._sendMail = sm;
            this._configuration = configuration;
            this._iAuthRepo = iAuthRepo;
            this._iLogger = logger;
        }
        public string OtpGenerator()
        {
            Random r = new Random();
            int newOtp = r.Next(100000, 999999);
            return newOtp.ToString();
        }
        private string GenerateAccessToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt-Data:Secret"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt-Data:Issuer"],
                audience: _configuration["Jwt-Data:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<User?> Create(UserCreateDto ucd)
        {
            var userExist = await _iAuthRepo.GetUserByEmail(ucd.Email);
            if (userExist != null) throw new ApiException(404, "User Already Exist");

            var otpExist = await _iAuthRepo.GetOtp(ucd.Email);
            
            if (otpExist == null || otpExist.ExpireAt < DateTime.UtcNow) throw new ApiException(404, "Otp does not exist");
            if (otpExist.Value != ucd.Otp) throw new ApiException(403, "Invalid otp");

            User? newUser = await _iAuthRepo.CreateUser(ucd);

            return newUser;
        }

        public async Task<string> Login(UserLoginDto uld)
        {
            User? user = await _iAuthRepo.GetUserByEmail(uld.Email);
            if(user == null) throw new ApiException(404, "User not found");
            if (user.Password != uld.Password) throw new ApiException(401, "Password Incorrect");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            return GenerateAccessToken(claims);
        }

        public async Task SendOtp(EmailDto email)
        {

            string otp = OtpGenerator();

            await _sendMail.MailSender(email.Email, "Otp for signup", $"<h1>{otp}</h1>");

            Otp newOtp = new Otp()
            {
                Value = otp,
                Email = email.Email
            };

            await _iAuthRepo.AddOtp(newOtp);
        }
    }
}
