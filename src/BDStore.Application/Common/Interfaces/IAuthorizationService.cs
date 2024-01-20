using BDStore.Application.Response;
using BDStore.Application.Tokens;
using BDStore.Domain.Users;

namespace BDStore.Application.Common.Interfaces
{
    public interface IAuthorizationService
    {
        Task<ApiResponse<TokenResponse>> Login(string username, string password);
        Task<ApiResponse<string>> Register(User user);
    }
}