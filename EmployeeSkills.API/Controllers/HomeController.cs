using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EmployeeSkills.API.Models;

namespace EmployeeSkills.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userManager.FindByIdAsync(userId ?? string.Empty);
                if (user == null) return Unauthorized();

                var roles = await _userManager.GetRolesAsync(user);
                return Ok(new
                {
                    message = $"Welcome {user.UserName}!",
                    email = user.Email,
                    roles = roles
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpGet("admin-only")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminOnly()
        {
            try
            {
                return Ok(new { message = "Admin access granted" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpGet("manager-or-admin")]
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult ManagerOrAdmin()
        {
            try
            {
                return Ok(new { message = "Manager or Admin access granted" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpGet("employee")]
        [Authorize(Roles = "Employee,Manager,Admin")]
        public IActionResult EmployeeAccess()
        {
            try
            {
                return Ok(new { message = "Employee access granted" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }
    }
}
