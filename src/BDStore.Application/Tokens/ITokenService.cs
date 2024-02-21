using Microsoft.AspNetCore.Identity;

namespace BDStore.Application.Tokens;

public interface ITokenService
{
    string GenerateToken(IdentityUser user);
}