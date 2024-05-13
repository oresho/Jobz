using JobzApi.Models.Entities;

namespace JobzApi.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(AppUser appUser);
        bool ValidateToken(string token);
    }
}
