using GoodToWork.Shared.AuthClient.Configuration;
using GoodToWork.Shared.AuthClient.DependencyConfiguration;
using GoodToWork.Shared.Common.Domain.Exceptions.Shared;
using GoodToWork.Shared.Common.Domain.Exceptions.Validation;
using GoodToWork.Shared.MessageBroker.DTOs.User;
using GoodToWork.Shared.MessageBroker.Infrastructure.Configuration;
using GoodToWork.TasksOrganizer.Application;
using GoodToWork.TasksOrganizer.Application.Configuration;
using GoodToWork.TasksOrganizer.Infrastructure.Configuration;
using GoodToWork.TasksOrganizer.Infrastructure.Exceptions;
using GoodToWork.TasksOrganizer.Infrastructure.Middlewares;
using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureApplicationLayer();

builder.Services.ConfigureInfrastructureLayer(
    builder.Configuration.GetConnectionString("TasksOrganizerTests"));

builder.Services.AddMessageBroker(c =>
{
    c.RegisterApplicationLayerAssembly(Assembly.GetAssembly(typeof(ApplicationEntryPoint)));
    c.RegisterConnectionUri(
        builder.Configuration["RabbitMqConfiguration:Host"], 
        builder.Configuration["RabbitMqConfiguration:UserName"], 
        builder.Configuration["RabbitMqConfiguration:Password"]);
    c.RegisterListener<UserUpdatedEvent>();
});

builder.Services.AddAuthClient(builder.Configuration.GetSection(AuthClientConfig.AuthServiceSection).Get<AuthClientConfig>());

builder.Services.AddCors();

var app = builder.Build();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c => c
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthMiddleware();

app.MapControllers();

app.Run();
