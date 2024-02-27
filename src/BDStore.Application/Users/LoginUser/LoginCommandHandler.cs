using BDStore.Application.Common.Interfaces;
using BDStore.Application.Response;
using BDStore.Application.Tokens;
using BDStore.Application.Users.Dto;
using MediatR;

namespace BDStore.Application.Users.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ApiResponse<UserResponseLogin>>
    {
        private readonly IAuthorizationService _authorizationService;
        public LoginUserCommandHandler(IAuthorizationService authenticationService)
        {
            _authorizationService = authenticationService;
        }

        public async Task<ApiResponse<UserResponseLogin>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.Login(request.UserName, request.Password);
            return result;
        }

    }
}