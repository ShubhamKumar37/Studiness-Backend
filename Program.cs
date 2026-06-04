using Microsoft.AspNetCore.Http.Features;
using Backend.Extensions;
using Serilog;
using Backend.logs;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
            try
            {
                Log.Information("Application is started and we are going live now");
                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog((context, services, configuration) =>
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext()
                        .Enrich.WithEnvironmentName()
                        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}")
                        .WriteTo.File(
                            path: "logs/onlystudy-.txt",
                            rollingInterval: RollingInterval.Day,
                            retainedFileCountLimit: 30,
                            outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}")
                );

                // Add services to the container.
                builder.Services.Configure<FormOptions>(options =>
                {
                    options.MultipartBodyLengthLimit = 100 * 1024 * 1024; // 100MB
                });

                // Kestrel limits
                builder.WebHost.ConfigureKestrel(options =>
                {
                    options.Limits.MaxRequestBodySize = 100 * 1024 * 1024;
                });

                builder.Services.AddAppSettings(builder.Configuration);
                builder.Services.AddDataBase(builder.Configuration);
                builder.Services.AddApplicationService();
                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddAllAuthentication(builder.Configuration);
                // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

                var app = builder.Build();
                app.UseMiddleware<ExceptionMiddleware>();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }


                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();
                app.MapGet("/", () => Results.Ok(new { status = "Studiness API is running", version = "1.0", timestamp = DateTime.UtcNow }));
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application startup Failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
