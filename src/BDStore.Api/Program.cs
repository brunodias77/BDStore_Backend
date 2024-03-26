using System.Reflection;
using BDStore.Api.Configurations;
using BDStore.Application.Abstractions.Mediator;
using BDStore.Application.Clients.Events;
using BDStore.Application.Users.RegisterUser;
using BDStore.Infra.Configurations;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.RegisterServices();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddInfraConfig(builder.Configuration);
// builder.Services.AddDataDependencyInjection(builder.Configuration);
builder.Services.AddMediatR(conf =>
    conf.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).GetTypeInfo().Assembly));
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
builder.Services.AddScoped<INotificationHandler<ClientRegistedEvent>, ClientEventHandler>();


var app = builder.Build();
app.UseApiConfiguration(app.Environment);
app.UseSwaggerConfiguration();
app.Run();