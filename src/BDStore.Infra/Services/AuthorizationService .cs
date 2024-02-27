using BDStore.Application.Common.Interfaces;
using BDStore.Application.Response;
using BDStore.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BDStore.Application.Users.Dto;
using BDStore.Domain.ValueObjects;
using Microsoft.Extensions.Options;


namespace BDStore.Infra.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;


        public AuthorizationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _appSettings = appSettings.Value;
        }

        public async Task<ApiResponse<UserResponseLogin>> Login(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var token = await GenerateToken(user);
                return new ApiResponse<UserResponseLogin>(token, "Usuário logado com sucesso");

            }

            return new ApiResponse<UserResponseLogin>("Erro ao fazer login do usuario");
        }

        public async Task<ApiResponse<string>> Register(User user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.Email.Value,
                Email = user.Email.Value,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            if (result.Succeeded)
            {
                return new ApiResponse<string>("Usuário registrado com sucesso.", "");
            }

            var errors = result.Errors.Select(e => e.Description);
            return new ApiResponse<string>("Erro ao registrar o usuário: " + string.Join(", ", errors));
        }

        public async Task<UserResponseLogin> GenerateToken(IdentityUser user)
        {

            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });
            var encodedToken = tokenHandler.WriteToken(token);
            var response = new UserResponseLogin
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationInHours).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };
            return response;
        }
        // public async Task<UserResponseLogin> GenerateToken(IdentityUser user)
        // {
        //     var claims = await _userManager.GetClaimsAsync(user);
        //     var identityClaims = await GetClaimsUser(claims, user);
        //     var encodedToken = CodifyToken(identityClaims);
        //     return new UserResponseLogin
        //     {
        //         AccessToken = encodedToken,
        //         ExpiresIn = 2,
        //         UserToken = new UserToken
        //         {
        //             Id = user.Id,
        //             Email = user.Email,
        //             Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
        //         }
        //     };
        // }
        //
        // private async Task<ClaimsIdentity> GetClaimsUser(ICollection<Claim> claims, IdentityUser user)
        // {
        //     var userRoles = await _userManager.GetRolesAsync(user);
        //     claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        //     claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        //     claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //     claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        //     claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
        //     foreach (var userRole in userRoles)
        //     {
        //         claims.Add(new Claim("role", userRole));
        //     }
        //     var identityClaims = new ClaimsIdentity();
        //     identityClaims.AddClaims(claims);
        //     return identityClaims;
        // }
        //
        // private string CodifyToken(ClaimsIdentity identityClaims)
        // {
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //     var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        //     {
        //         Issuer = _appSettings.Issuer,
        //         Audience = _appSettings.ValidIn,
        //         Subject = identityClaims,
        //         Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationInHours),
        //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //     });
        //     return tokenHandler.WriteToken(token);
        // }

        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        // public string GenerateToken(IdentityUser user)
        // {
        //     var secretKey =
        //         new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"] ?? string.Empty));
        //     var issuer = _configuration["JwtSettings:Issuer"];
        //     var audience = _configuration["JwtSettings:Audience"];
        //
        //     var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        //     var tokenOptions = new JwtSecurityToken(
        //         issuer: issuer,
        //         audience: audience,
        //         claims: new[]
        //         {
        //             new Claim(type: ClaimTypes.Name, user.UserName)
        //         },
        //         expires: DateTime.Now.AddHours(2),
        //         signingCredentials: signInCredentials
        //     );
        //     var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        //     return token;
        // }
    }
}