using MediatR;

namespace BDStore.Application.Abstractions.Commands
{
    // Interface base para comandos sem resultado específico
    public interface ICommand : IRequest
    {
        // Propriedade que representa o identificador único do comando
        Guid Id { get; }
    }

    // Interface para comandos com um resultado específico
    // Usa covariância (out) para permitir que o tipo de resultado seja mais específico
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        // Propriedade que representa o identificador único do comando
        Guid Id { get; }
    }
}