using BDStore.Application.Abstractions.Mediator;
using BDStore.Application.Clients.RegisterClient;
using Microsoft.AspNetCore.Mvc;

namespace BDStore.Api.Controllers;

public class ClientController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;

    public ClientController(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    [HttpGet("clients")]
    public async Task<IActionResult> Index()
    {
        var result = await _mediatorHandler.SendCommand(new RegisterClientCommand(Guid.NewGuid(), "Bruno Henrique Dias",
            "henrique@teste.com", "91532363001"));
        return CustomResponse(result);
    }
}