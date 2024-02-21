// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using BDStore.Application.Tokens;
// using BDStore.Domain.Users;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.Extensions.Configuration;
// using Microsoft.IdentityModel.Tokens;
//
// namespace BDStore.Infra.Services;
//
// public class TokenService : ITokenService
// {
//     public TokenService(IConfiguration configuration, UserManager<IdentityUser> userManager)
//     {
//         _configuration = configuration;
//         _userManager = userManager;
//     }
//
//     private readonly IConfiguration _configuration;
//     private readonly UserManager<IdentityUser> _userManager;
//
//
// // Método para gerar um token JWT para um usuário
//     public async Task<string> GenerateToken(IdentityUser user)
//     {
//         var claims = await _userManager.GetClaimsAsync(user);
//         // Obter a chave secreta do arquivo de configuração (se não estiver definida, usará uma string vazia)
//         var secretKey =
//             new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"] ?? string.Empty));
//
//         // Obter o emissor do arquivo de configuração
//         var issuer = _configuration["JwtSettings:Issuer"];
//
//         // Obter o público-alvo do arquivo de configuração
//         var audience = _configuration["JwtSettings:Audience"];
//
//         // Criar as credenciais de assinatura usando a chave secreta e o algoritmo HmacSha256
//         var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
//
//         // Definir as opções do token JWT, incluindo emissor, público-alvo, reivindicações, tempo de expiração e credenciais de assinatura
//         var tokenOptions = new JwtSecurityToken(
//             issuer: issuer,
//             audience: audience,
//             claims: new[]
//             {
//                 // Definir as reivindicações do token, neste caso, apenas o nome do usuário
//                 new Claim(type: ClaimTypes.Name, user.UserName)
//             },
//             expires: DateTime.Now.AddHours(2), // Definir a expiração do token (2 horas a partir do momento atual)
//             signingCredentials: signInCredentials // Usar as credenciais de assinatura
//         );
//
//         // Escrever o token JWT com base nas opções fornecidas
//         var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
//
//         // Retornar o token gerado
//         return token;
//     }
//
//     // Método para converter uma data e hora para o formato de data Unix(Epoch)
//     // O formato de data Unix (Epoch) representa a quantidade de segundos decorridos desde 1º de janeiro de 1970 às 00:00:00 UTC
//     // Esse método recebe uma data e hora como parâmetro e retorna o valor em segundos desde o Epoch
//     private static long ToUnixEpochDate(DateTime date) =>
//         (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
//             .TotalSeconds);
//
//
//     private async Task<ClaimsIdentity> GetClaimsUser(ICollection<Claim> claims, IdentityUser user)
//     {
//         var userRoles = await _userManager.GetRolesAsync(user);
//         claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
//         claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
//         claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
//         claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
//         claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(),
//             ClaimValueTypes.Integer64));
//         foreach (var userRole in userRoles)
//         {
//             claims.Add(new Claim("role", userRole));
//         }
//
//         var identityClaims = new ClaimsIdentity();
//         identityClaims.AddClaims(claims);
//         return identityClaims;
//     }
//
//     private string CodifyToken(ClaimsIdentity identityClaims)
//     {
//         var tokenHandler = new JwtSecurityTokenHandler();
//         var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);
//         var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
//         {
//             Issuer = _configuration["JwtSettings:Issuer"],
//             Audience = _configuration["JwtSettings:Audience"],
//             Subject = identityClaims,
//             Expires = DateTime.UtcNow.AddHours(2),
//             SigningCredentials = new SigningCredentials()
//         });
//         return tokenHandler.WriteToken(token);
//     }
// }

