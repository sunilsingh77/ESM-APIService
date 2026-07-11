using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EmployeeSkills.Application.DTOs;
using EmployeeSkillsSummary.Domain.Entities;

namespace EmployeeSkills.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                    return BadRequest("Email and password are required");

                var user = new ApplicationUser { UserName = dto.UserName, Email = dto.Email };

                var result = await _userManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                if (dto.Roles != null && dto.Roles.Count > 0)
                {
                    foreach (var role in dto.Roles)
                    {
                        if (!await _roleManager.RoleExistsAsync(role))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(role));
                        }

                        await _userManager.AddToRoleAsync(user, role);
                    }
                }

                return Ok(new { message = "User registered successfully", email = dto.Email, roles = dto.Roles });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                    return BadRequest("Email and password are required");

                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                    return Unauthorized(new { message = "Invalid email or password" });

                var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
                if (!passwordValid)
                    return Unauthorized(new { message = "Invalid email or password" });

                await _signInManager.SignInAsync(user, false);

                return Ok(new { message = "Login successful", email = user.Email, userId = user.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok(new { message = "Logged out successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }
    }
}
