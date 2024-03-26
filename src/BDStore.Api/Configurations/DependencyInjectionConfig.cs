using BDStore.Application.Abstractions.Mediator;
using BDStore.Application.Clients.Events;
using MediatR;

namespace BDStore.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<ClientRegistedEvent>, ClientEventHandler>();
            // services
            //     .AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>, RegisterClientCommandHandler>();
        }
    }
}