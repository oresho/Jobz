using JobzApi.ExceptionHandler.CustomExceptions;
using JobzApi.Models.Dtos.Requests;
using JobzApi.Models.Dtos.Responses;
using JobzApi.Models.Entities;
using JobzApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace JobzApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<GenericApiResponse> CreateUserAsync(RegisterUserRequest userRequest)
        {
            var existingUser = await _userManager.FindByNameAsync(userRequest.Email);
            if (existingUser != null)
            {
                throw new IllegalArgumentException("This email is already being used");
            }
            AppUser user = new AppUser()
            {
                Email = userRequest.Email,
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                PhoneNumber = userRequest.PhoneNumber,
                UserName = userRequest.Email
            };
            var result = await _userManager.CreateAsync(user, userRequest.Password);
            if (!result.Succeeded)
            {
                throw new InternalServerException("Could not create user");
            }
            await _userManager.AddToRoleAsync(user, userRequest.Role);
            return new GenericApiResponse() { Success = true, StatusCode = 201, Message = "Successfully Created User" };
        }

        public async Task<GenericApiResponse> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ResourceNotFoundException("User does not exist");
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InternalServerException("Could not delete user");
            }
            return new GenericApiResponse() { Success = true, StatusCode = 204, Message = "Successfully Deleted User" };
        }

        public async Task<GenericApiResponse<IEnumerable<UserResponse>>> GetAllUsersAsync(int page, int pageSize)
        {
            var users = _userManager.Users.Skip((page - 1) * pageSize).Take(pageSize).Select(x => MapToUserResponse(x)).ToList();
            return new GenericApiResponse<IEnumerable<UserResponse>>() { Success = true, StatusCode = 200, Message = $"Successfully Gotten Users. Page: {page}, Total Record Count: {_userManager.Users.Count()}", Data = users };
        }

        public async Task<GenericApiResponse<IEnumerable<UserResponse>>> GetAllUsersBySearchTerm(string searchTerm, string value, int page, int pageSize)
        {
            var users = _userManager.Users;
            IOrderedQueryable<AppUser> orderedUsers;
            var term = searchTerm.ToLower();
            var valStr = value.ToLower();
            switch (term)
            {
                case "firstname":
                    orderedUsers = users.Where(x => x.FirstName.ToLower().Contains(valStr)).OrderBy(x => x.FirstName);
                    break;
                case "lastname":
                    orderedUsers = users.Where(x => x.LastName.ToLower().Contains(valStr)).OrderBy(x => x.LastName);
                    break;
                case "email":
                    orderedUsers = users.Where(x => x.Email.ToLower().Contains(valStr)).OrderBy(x => x.Email);
                    break;
                case "phonenumber":
                    orderedUsers = users.Where(x => x.PhoneNumber.ToLower().Contains(valStr)).OrderBy(x => x.PhoneNumber);
                    break;
                default:
                    orderedUsers = users.Where(x => x.FirstName.ToLower().Contains(valStr)).OrderBy(x => x.FirstName);
                    break;
            }
            var response = orderedUsers.Select(MapToUserResponse).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new GenericApiResponse<IEnumerable<UserResponse>>() { Success = true, StatusCode = 200, Message = $"Successfully Gotten Users. Page: {page}, Total Record Count: {orderedUsers.Count()}", Data = response };
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new ResourceNotFoundException("User with this email does not exist");
            }
            return user;
        }

        public async Task<GenericApiResponse<UserResponse>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ResourceNotFoundException("User does not exist");
            }
            UserResponse userResponse = MapToUserResponse(user);
            return new GenericApiResponse<UserResponse>() { Success = true, StatusCode = 200, Message = "Successfully Gotten User", Data = userResponse };
        }

        private static UserResponse MapToUserResponse(AppUser user)
        {
            return new UserResponse() { Id = user.Id, FirstName = user.FirstName, Email = user.Email, LastName = user.LastName, PhoneNumber = user.PhoneNumber };
        }

        public async Task<GenericApiResponse> UpdateUserAsync(string id, UpdateUserRequest userRequest)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ResourceNotFoundException("User does not exist");
            }
            user.FirstName = userRequest.FirstName.IsNullOrEmpty() ? user.FirstName : userRequest.FirstName;
            user.LastName = userRequest.LastName.IsNullOrEmpty() ? user.LastName : userRequest.LastName;
            user.PhoneNumber = userRequest.PhoneNumber.IsNullOrEmpty() ? user.PhoneNumber : userRequest.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new InternalServerException("Could not update user");
            }
            return new GenericApiResponse() { Success = true, StatusCode = 200, Message = "Successfully Updated User" };
        }
    }
}
