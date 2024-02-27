using BDStore.Application.Response;
using BDStore.Application.Users.Dto;
using BDStore.Domain.Users;

namespace BDStore.Application.Common.Interfaces
{
    public interface IAuthorizationService
    {
        Task<ApiResponse<UserResponseLogin>> Login(string username, string password);
        Task<ApiResponse<string>> Register(User user);
    }
}