using BDStore.Application.Common.Interfaces;
using BDStore.Application.Response;
using BDStore.Application.Tokens;
using BDStore.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BDStore.Infra.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;


        public AuthorizationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<ApiResponse<TokenResponse>> Login(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var token = GenerateToken(user);
                return new ApiResponse<TokenResponse>(
                    new TokenResponse("meuToken", DateTime.UtcNow.AddDays(1), "nomeDeUsuario"),
                    "Usuario fez login com sucesso");
            }

            return new ApiResponse<TokenResponse>("Erro ao fazer login do usuario");
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
                return new ApiResponse<string>("Usuário registrado com sucesso.");
            }

            var errors = result.Errors.Select(e => e.Description);
            return new ApiResponse<string>("Erro ao registrar o usuário: " + string.Join(", ", errors));
        }

        public string GenerateToken(IdentityUser user)
        {
            var secretKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"] ?? string.Empty));
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new[]
                {
                    new Claim(type: ClaimTypes.Name, user.UserName)
                },
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signInCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }
    }
}