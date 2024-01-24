using BDStore.Application.Common.Interfaces;
using BDStore.Application.Response;
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

        public Task<ApiResponse<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("cheguei aqui");
            Console.WriteLine(request.FirstName);
            // Implemente a lógica aqui e retorne um valor
            throw new NotImplementedException();
        }
    }
}
