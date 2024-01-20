namespace BDStore.Application.Common.Interfaces
{
    public class IAuthorizationService
    {
        Task<ApiResponse<TokenResponse>> Login(string username, string password);
        Task<ApiResponse<string>> Register(User user);
    }
}