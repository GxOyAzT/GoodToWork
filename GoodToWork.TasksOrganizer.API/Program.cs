using GoodToWork.Shared.MessageBroker.DTOs.User;
using GoodToWork.Shared.MessageBroker.Infrastructure.Configuration;
using GoodToWork.TasksOrganizer.Application;
using GoodToWork.TasksOrganizer.Application.Configuration;
using GoodToWork.TasksOrganizer.Infrastructure.Configuration;
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
    c.RegisterConnectionUri(builder.Configuration["RabbitMqConfiguration:ConnectionUri"]);
    c.RegisterListener<UserUpdatedEvent>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
