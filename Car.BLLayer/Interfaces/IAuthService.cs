using Car.BLLayer.DTO.RequestDtos;
using Car.BLLayer.DTO.ResponseDto;
using Microsoft.AspNetCore.Identity;

namespace Car.BLLayer.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterDto register);
        Task<AuthResponse> UserLogin(LoginDto loginDto);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto changePassword);
        Task Logout();

    }
}
