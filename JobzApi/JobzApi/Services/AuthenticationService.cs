using JobzApi.ExceptionHandler.CustomExceptions;
using JobzApi.Models.Dtos.Requests;
using JobzApi.Models.Dtos.Responses;
using JobzApi.Models.Entities;
using JobzApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JobzApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService;
        public AuthenticationService(IUserService userService, SignInManager<AppUser> signInManager, IJwtService jwtService)
        {
            _userService = userService;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<GenericApiResponse<string>> LoginUserAsync(LoginUserRequest request)
        {
            var user = await _userService.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ResourceNotFoundException("User does not exist");
            }
            var response = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
            if (!response.Succeeded) 
            {
                throw new UnauthorizedAccessException("Invalid Credentials");
            }
            string token = await _jwtService.GenerateTokenAsync(user);
            var apiResponse = new GenericApiResponse<string> { Success = true, StatusCode = 200, Message = "Successfully logged in user", Data = token };
            return apiResponse;
        }

        public async Task<GenericApiResponse> RegisterUserAsync(RegisterUserRequest request)
        {
            return await _userService.CreateUserAsync(request);
        }
    }
}
