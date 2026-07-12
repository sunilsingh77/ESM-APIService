using EmployeeSkills.Application.Authentication.DTOs;
using System.Security.Claims;

namespace EmployeeSkills.Application.Authentication.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResponse> RegisterAsync(UserRegisterRequest dto);
        Task<AuthenticationResponse> LoginAsync(UserLoginRequest dto);
        Task<AuthenticationResponse> RefreshTokenAsync(RefreshTokenRequest dto);
        Task LogoutAsync(RefreshTokenRequest dto);
        Task<UserProfileResponse> ValidateAsync(ClaimsPrincipal user);
    }
}


