using System.ComponentModel.DataAnnotations;
using BDStore.Application.Response;
using BDStore.Application.Tokens;
using MediatR;

namespace BDStore.Application.Users.LoginUser;

public class LoginUserCommand : IRequest<ApiResponse<TokenResponse>>
{
    [Required(ErrorMessage = "Nome do usuario e obrigatorio")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Senha do usuario e obrigatoria")]
    public string Password { get; set; }

    public LoginUserCommand(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}