using JobzApi.Models.Dtos.Requests;
using JobzApi.Models.Dtos.Responses;

namespace JobzApi.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<GenericApiResponse> RegisterUserAsync(RegisterUserRequest request);
        Task<GenericApiResponse<string>> LoginUserAsync(LoginUserRequest request);
    }
}
