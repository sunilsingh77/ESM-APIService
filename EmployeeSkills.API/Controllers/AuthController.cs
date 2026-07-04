using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EmployeeSkills.API.Models;
using EmployeeSkills.API.Data;
using EmployeeSkills.API.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeSkills.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EmployeeSkillsDbContext _dbContext;
        private readonly JwtService _jwtService;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, EmployeeSkillsDbContext dbContext, JwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _jwtService = jwtService;
        }

        public class LoginRequest { public string Email { get; set; } = string.Empty; public string Password { get; set; } = string.Empty; }
        public class AuthResponse { public string AccessToken { get; set; } = string.Empty; public string RefreshToken { get; set; } = string.Empty; }
        public class RefreshRequest { public string RefreshToken { get; set; } = string.Empty; }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null) return Unauthorized("Invalid credentials");

                if (!await _userManager.CheckPasswordAsync(user, model.Password)) return Unauthorized("Invalid credentials");

                var roles = await _userManager.GetRolesAsync(user);
                var accessToken = _jwtService.GenerateAccessToken(user, roles);
                var refreshToken = _jwtService.GenerateRefreshToken();

                var rt = new RefreshToken
                {
                    Token = refreshToken,
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    UserId = user.Id
                };

                _dbContext.RefreshTokens.Add(rt);
                await _dbContext.SaveChangesAsync();

                return Ok(new AuthResponse { AccessToken = accessToken, RefreshToken = refreshToken });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest model)
        {
            try
            {
                var existing = await _dbContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == model.RefreshToken);
                if (existing == null || !existing.IsActive) return Unauthorized("Invalid refresh token");

                var user = await _userManager.FindByIdAsync(existing.UserId);
                if (user == null) return Unauthorized();

                existing.Revoked = DateTime.UtcNow;

                var roles = await _userManager.GetRolesAsync(user);
                var accessToken = _jwtService.GenerateAccessToken(user, roles);
                var refreshToken = _jwtService.GenerateRefreshToken();

                var newRt = new RefreshToken
                {
                    Token = refreshToken,
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    UserId = user.Id
                };

                _dbContext.RefreshTokens.Add(newRt);
                await _dbContext.SaveChangesAsync();

                return Ok(new AuthResponse { AccessToken = accessToken, RefreshToken = refreshToken });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshRequest model)
        {
            try
            {
                var existing = await _dbContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == model.RefreshToken);
                if (existing == null) return NotFound();

                existing.Revoked = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }
    }
}
