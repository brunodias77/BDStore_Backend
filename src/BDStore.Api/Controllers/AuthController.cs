using BDStore.Application.Users.LoginUser;
using BDStore.Application.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace BDStore.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class AuthController : MainController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var response = await _mediator.Send(new LoginUserCommand(request.UserName, request.Password));
            if (!response.Success)
            {
                return Unauthorized(response.Message);
            }

            return Ok(response.Data.Token);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Status = "Error", Message = "Erro de validação", Errors = errors });
            }

            var response = await _mediator.Send(request);
            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Status = "Error", Message = "Não foi possível criar o usuário" });
            }

            return Ok(new { Status = "Success", Message = "Usuário criado com sucesso!", Token = response.Data });
        }
    }
}