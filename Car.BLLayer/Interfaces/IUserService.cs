using Car.BLLayer.DTO.RequestDtos;
using Car.BLLayer.DTO.ResponseDto;
using Microsoft.AspNetCore.JsonPatch;

namespace Car.BLLayer.Interfaces
{
    public interface IUserService
    {
        Task<ProfileResponse> GetUserProfileAsync(string userId);
        Task<bool> PartialUpdateUserAsync(string userId, JsonPatchDocument<UpdateRequestDto> patchDoc);
        Task<bool> DeleteAsync(string userId);
        Task<bool> UpdateUserAsync(string id, UpdateRequestDto model);
        Task<bool> DeactivateAsync(string userId);
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
