
using EmployeeSkills.Application.Authentication.DTOs;
using EmployeeSkills.Application.DTOs.Authentication;
using EmployeeSkills.Application.Interfaces;
using EmployeeSkills.Domain.Entities;
using EmployeeSkills.Infrastructure.Persistence;
using EmployeeSkillsSummary.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace EmployeeSkills.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly EmployeeSkillsDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        EmployeeSkillsDbContext context,
        IJwtService jwtService,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _context = context;
        _jwtService = jwtService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthenticationResponse> LoginAsync(UserLoginRequest dto)
    {
        // Find user by email
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password.");

        // Validate password
        var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!passwordValid)
            throw new UnauthorizedAccessException("Invalid email or password.");

        // Sign in (optional if you're using JWT only)
        await _signInManager.SignInAsync(user, isPersistent: false);

        // Get user roles
        var roles = await _userManager.GetRolesAsync(user);

        // Generate JWT
        var accessToken = _jwtService.GenerateAccessToken(user, roles);

        // Generate Refresh Token
        var refreshToken = _jwtService.GenerateRefreshToken();

        // Save Refresh Token
        var token = new RefreshToken(
            refreshToken,
            DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
            user.Id);

        _context.RefreshTokens.Add(token);

        await _context.SaveChangesAsync();

        return new AuthenticationResponse
        {
            Success = true,
            Message = "Login successful.",

            UserId = user.Id,
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            Roles = roles,

            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiration =
                DateTime.UtcNow.AddMinutes(
                    _jwtSettings.AccessTokenExpirationMinutes)
        };
    }

    public async Task LogoutAsync(RefreshTokenRequest request)
    {
        var refreshToken = await _context.RefreshTokens
            .SingleOrDefaultAsync(x => x.Token == request.RefreshToken);

        if (refreshToken == null)
            throw new UnauthorizedAccessException("Refresh token not found.");

        if (!refreshToken.IsActive)
            return;

        refreshToken.Revoke();

        await _context.SaveChangesAsync();
    }
    public async Task<AuthenticationResponse> RefreshTokenAsync(RefreshTokenRequest request)
    {
        // 1. Find refresh token
        var existingToken = await _context.RefreshTokens
            .SingleOrDefaultAsync(x => x.Token == request.RefreshToken);

        if (existingToken == null)
            throw new UnauthorizedAccessException("Invalid refresh token.");

        if (!existingToken.IsActive)
            throw new UnauthorizedAccessException("Refresh token has expired.");

        // 2. Find user
        var user = await _userManager.FindByIdAsync(existingToken.UserId);

        if (user == null)
            throw new UnauthorizedAccessException("User not found.");

        // 3. Revoke current token
        existingToken.Revoke();

        // 4. Revoke ALL other active tokens for this user
        var activeTokens = await _context.RefreshTokens
            .Where(x => x.UserId == user.Id &&
                        x.Revoked == null)
            .ToListAsync();

        foreach (var token in activeTokens)
        {
            token.Revoke();
        }

        // 5. Get Roles
        var roles = await _userManager.GetRolesAsync(user);

        // 6. Generate new Access Token
        var accessToken = _jwtService.GenerateAccessToken(user, roles);

        // 7. Generate new Refresh Token
        var refreshToken = _jwtService.GenerateRefreshToken();

        // 8. Save new Refresh Token
        var newToken = new RefreshToken(
            refreshToken,
            DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
            user.Id);

        _context.RefreshTokens.Add(newToken);

        await _context.SaveChangesAsync();

        // 9. Return response
        return new AuthenticationResponse
        {
            Success = true,
            Message = "Token refreshed successfully.",
            UserId = user.Id,
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            Roles = roles,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiration = DateTime.UtcNow.AddMinutes(
                _jwtSettings.AccessTokenExpirationMinutes)
        };
    }
    public async Task<AuthenticationResponse> RegisterAsync(UserRegisterRequest dto)
    {
        // Check email already exists
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);

        if (existingUser != null)
        {
            throw new ApplicationException("Email already exists.");
        }

        // Create Identity user
        var user = new ApplicationUser
        {
            UserName = dto.UserName,
            Email = dto.Email,
            FullName = dto.FullName,
            CreatedDate = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            throw new ApplicationException(
                string.Join(", ", result.Errors.Select(x => x.Description)));
        }

        // Create roles if missing
        if (dto.Roles != null)
        {
            foreach (var role in dto.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }

            await _userManager.AddToRolesAsync(user, dto.Roles);
        }

        // Get assigned roles
        var roles = await _userManager.GetRolesAsync(user);

        // Generate JWT
        var accessToken =
            _jwtService.GenerateAccessToken(user, roles);

        // Generate Refresh Token
        var refreshToken =
            _jwtService.GenerateRefreshToken();

        // Save Refresh Token
        var token = new RefreshToken(
            refreshToken,
            DateTime.UtcNow.AddDays(
                _jwtSettings.RefreshTokenExpirationDays),
            user.Id);

        _context.RefreshTokens.Add(token);

        await _context.SaveChangesAsync();

        return new AuthenticationResponse
        {
            Success = true,
            Message = "User registered successfully.",
            UserId = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            Roles = roles,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes)
        };
    }
    public async Task<UserProfileResponse> ValidateAsync(
    ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);

        if (user == null)
            throw new UnauthorizedAccessException();
        var roles = await _userManager.GetRolesAsync(user);

        return new UserProfileResponse
        {
            UserId = user.Id,
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            Roles = (List<string>)roles
        };
    }
}

//using EmployeeSkillsSummary.Domain.Entities;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace EmployeeSkills.Application.Services
//{
//    public class AuthService
//    {
//        private readonly IConfiguration _config;

//        public AuthService(IConfiguration config)
//        {
//            _config = config;
//        }
//        public string GenerateAccessToken(ApplicationUser user, IList<string> roles)
//        {
//            var jwtSection = _config.GetSection("Jwt");
//            var key = Encoding.UTF8.GetBytes(jwtSection["Key"] ?? string.Empty);
//            var issuer = jwtSection["Issuer"];
//            var audience = jwtSection["Audience"];
//            var expiryMinutes = int.TryParse(jwtSection["ExpiryMinutes"], out var m) ? m : 60;

//            var claims = new List<Claim>
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
//                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? string.Empty),
//                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//            };

//            foreach (var role in roles)
//            {
//                claims.Add(new Claim(ClaimTypes.Role, role));
//            }

//            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
//            var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.UtcNow.AddMinutes(expiryMinutes), signingCredentials: credentials);
//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//        public string GenerateRefreshToken()
//        {
//            var randomNumber = new byte[64];
//            using var rng = RandomNumberGenerator.Create();
//            rng.GetBytes(randomNumber);
//            return Convert.ToBase64String(randomNumber);
//        }
//    }
//}
