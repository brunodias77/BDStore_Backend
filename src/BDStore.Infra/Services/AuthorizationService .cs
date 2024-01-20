using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BDStore.Application.Common.Interfaces;
using BDStore.Application.Response;
using BDStore.Application.Tokens;
using BDStore.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BDStore.Infra.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        readonly UserManager<IdentityUser> _userManager;
        readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthorizationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<ApiResponse<TokenResponse>> Login(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = CreateToken(user);
                return new ApiResponse<TokenResponse>(new TokenResponse("meuToken", DateTime.UtcNow.AddDays(1), "nomeDeUsuario"), "Usuario fez login com sucesso");
            }
            else
            {
                // Construa uma resposta de falha
                return new ApiResponse<TokenResponse>("Erro ao fazer login do usuario");
            }
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
                // Aqui, você pode enviar um email de confirmação ou realizar outras ações necessárias após o registro.
                // Por exemplo, adicionar roles ao usuário, se necessário.

                return new ApiResponse<string>("Usuário registrado com sucesso.");
            }
            else
            {
                // Aqui, lidamos com os erros que podem ocorrer durante o registro
                var errors = result.Errors.Select(e => e.Description);
                return new ApiResponse<string>("Erro ao registrar o usuário: " + string.Join(", ", errors));
            }
        }

        public TokenResponse CreateToken(IdentityUser user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!)
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Secret").Value));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenResponse(
                tokenHandler.WriteToken(token),
                tokenDescriptor.Expires, // Passa a data de expiração para o TokenResponse
                user.UserName
            );
        }
    }
}