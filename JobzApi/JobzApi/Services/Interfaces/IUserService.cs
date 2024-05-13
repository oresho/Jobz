using JobzApi.Models.Dtos.Requests;
using JobzApi.Models.Dtos.Responses;
using JobzApi.Models.Entities;

namespace JobzApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<GenericApiResponse> CreateUserAsync(RegisterUserRequest userRequest);
        Task<GenericApiResponse> UpdateUserAsync(string id, UpdateUserRequest userRequest);
        Task<GenericApiResponse> DeleteUserAsync(string id);
        Task<GenericApiResponse<IEnumerable<UserResponse>>> GetAllUsersAsync(int page, int pageSize);
        Task<GenericApiResponse<UserResponse>> GetUserById(string id);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task<GenericApiResponse<IEnumerable<UserResponse>>> GetAllUsersBySearchTerm(string searchTerm, string value, int page, int pageSize);
    }
}
