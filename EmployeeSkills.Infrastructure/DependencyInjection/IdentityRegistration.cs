using EmployeeSkills.Application.Authentication.DTOs;
using EmployeeSkills.Application.Authentication.Interfaces;
using EmployeeSkills.Domain.Entities;
using EmployeeSkills.Infrastructure.Authentication;
using EmployeeSkills.Infrastructure.Persistence;
using EmployeeSkillsSummary.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeeSkills.Infrastructure.DependencyInjection;

public static class IdentityRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Identity
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // Password
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;
            // User
            options.User.RequireUniqueEmail = true;
            // Lockout
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
            // SignIn
            options.SignIn.RequireConfirmedAccount = false;
        })
        .AddEntityFrameworkStores<EmployeeSkillsDbContext>()
        .AddDefaultTokenProviders();

        // JWT Configuration
        services.Configure<JwtSettings>(
            configuration.GetSection(JwtSettings.SectionName));

        var jwtSettings = configuration
            .GetSection(JwtSettings.SectionName)
            .Get<JwtSettings>()!;

        var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

        services.AddAuthentication(options =>
                                    {
                                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                                    })
                                    .AddJwtBearer(options =>
                                    {
                                        options.RequireHttpsMetadata = false;
                                        options.SaveToken = true;
                                        options.TokenValidationParameters =
                                                                        new TokenValidationParameters
                                                                        {
                                                                            ValidateIssuer = true,
                                                                            ValidateAudience = true,
                                                                            ValidateLifetime = true,
                                                                            ValidateIssuerSigningKey = true,
                                                                            ValidIssuer = jwtSettings.Issuer,
                                                                            ValidAudience = jwtSettings.Audience,
                                                                            IssuerSigningKey = new SymmetricSecurityKey(key),
                                                                            ClockSkew = TimeSpan.Zero
                                                                        };
                                    });

        services.AddAuthorization();
        // Authentication Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}