


using BDStore.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
// builder.Services.AddDataDependencyInjection(builder.Configuration);
builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).GetTypeInfo().Assembly));



var app = builder.Build();
app.UseApiConfiguration(app.Environment);
app.UseSwaggerConfiguration();
app.Run();

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();

