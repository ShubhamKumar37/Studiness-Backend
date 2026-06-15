using Backend.Application.Auth.Interfaces;
using Backend.Application.Auth.Repositories;
using Backend.Application.Auth.Services;
using Backend.Application.Course.Interfaces;
using Backend.Application.Course.Repositories;
using Backend.Application.Course.Services;
using Backend.Application.CourseSection.Interfaces;
using Backend.Application.CourseSection.Repositories;
using Backend.Application.CourseSection.Services;
using Backend.Data;
using Backend.Settings;
using Backend.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Backend.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<SendMail>();
            services.AddScoped<IAuthRepo, AuthRepo>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICourseRepo, CourseRepo>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICloudinaryUtils, CloudinaryUtils>();
            services.AddScoped<ICourseSectionRepo, CourseSectionRepo>();
            services.AddScoped<ICourseSectionService, CourseSectionService>();

            return services;
        }

        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }

        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(
                configuration.GetSection("EmailSettings")
            );
            services.Configure<CloudinarySettings>(
                configuration.GetSection("CloudinarySettings")
            );

            return services;
        }

        public static IServiceCollection AddAllAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme
            )
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["Jwt-Data:Issuer"],
                        ValidAudience = configuration["Jwt-Data:Audience"],

                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["Jwt-Data:Secret"]!)
                            )
                    };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token =
                            context.Request.Cookies["token"];

                        return Task.CompletedTask;
                    }
                };


            });

            return services;
        }
    }
}
