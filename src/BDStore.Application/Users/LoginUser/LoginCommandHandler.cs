using BDStore.Application.Common.Interfaces;
using BDStore.Application.Response;
using BDStore.Application.Tokens;
using MediatR;

namespace BDStore.Application.Users.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ApiResponse<TokenResponse>>
    {
        private readonly IAuthorizationService _authorizationService;
        public LoginUserCommandHandler(IAuthorizationService authenticationService)
        {
            _authorizationService = authenticationService;
        }

        public async Task<ApiResponse<TokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.Login(request.UserName, request.Password);
            return result;
        }

    }
}