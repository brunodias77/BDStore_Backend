using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BDStore.Application.Tokens;
using BDStore.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BDStore.Infra.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

// Método para gerar um token JWT para um usuário
    public string GenerateToken(IdentityUser user)
    {
        // Obter a chave secreta do arquivo de configuração (se não estiver definida, usará uma string vazia)
        var secretKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"] ?? string.Empty));

        // Obter o emissor do arquivo de configuração
        var issuer = _configuration["JwtSettings:Issuer"];

        // Obter o público-alvo do arquivo de configuração
        var audience = _configuration["JwtSettings:Audience"];

        // Criar as credenciais de assinatura usando a chave secreta e o algoritmo HmacSha256
        var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        // Definir as opções do token JWT, incluindo emissor, público-alvo, reivindicações, tempo de expiração e credenciais de assinatura
        var tokenOptions = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: new[]
            {
                // Definir as reivindicações do token, neste caso, apenas o nome do usuário
                new Claim(type: ClaimTypes.Name, user.UserName)
            },
            expires: DateTime.Now.AddHours(2), // Definir a expiração do token (2 horas a partir do momento atual)
            signingCredentials: signInCredentials // Usar as credenciais de assinatura
        );

        // Escrever o token JWT com base nas opções fornecidas
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        // Retornar o token gerado
        return token;
    }
}