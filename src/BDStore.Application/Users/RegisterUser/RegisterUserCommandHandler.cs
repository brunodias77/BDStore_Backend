using BDStore.Application.Common.Interfaces;
using BDStore.Application.Response;
using BDStore.Domain.Users;
using BDStore.Domain.Users.ValueObjects;
using MediatR;


namespace BDStore.Application.Users.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ApiResponse<string>>
    {
        private readonly IAuthorizationService _authorizationService;

        public RegisterUserCommandHandler(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<ApiResponse<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = User.CreateRegistered(new FirstName(request.FirstName), new LastName(request.LastName),
                new Email(request.Email), request.Password);
            var response = await _authorizationService.Register(user);
            return response;
        }
    }
}