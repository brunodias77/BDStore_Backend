using Microsoft.AspNetCore.Identity;

namespace BDStore.Application.Tokens;

public interface ITokenService
{
    Task<string> GenerateToken(IdentityUser user);
}