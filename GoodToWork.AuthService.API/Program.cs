using GoodToWork.AuthService.API.Grpc.Listeners;
using GoodToWork.AuthService.Application;
using GoodToWork.AuthService.Infrastructure.DependencyConfiguration;
using GoodToWork.AuthService.Infrastructure.Middlewares;
using GoodToWork.Shared.MessageBroker.Infrastructure.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();
builder.Services.AddInfrastructure(builder.Configuration, typeof(ApplicationEntryPoint).Assembly);

builder.Services.AddMessageBroker(config =>
{
    config.RegisterApplicationLayerAssembly(Assembly.GetAssembly(typeof(ApplicationEntryPoint)));

    config.RegisterConnectionUri(
        builder.Configuration["RabbitMqConfiguration:Host"],
        builder.Configuration["RabbitMqConfiguration:UserName"],
        builder.Configuration["RabbitMqConfiguration:Password"]);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuth();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<ValidateTokenListener>();
});

app.Run();
