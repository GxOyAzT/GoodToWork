using GoodToWork.NotificationService.Application;
using GoodToWork.NotificationService.Application.Configuration;
using GoodToWork.NotificationService.Infrastructure.Emailer.Configuration;
using GoodToWork.NotificationService.Infrastructure.Persistance.Configuration;
using GoodToWork.Shared.MessageBroker.DTOs.Email;
using GoodToWork.Shared.MessageBroker.Infrastructure.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistanceLayer(
    builder.Configuration
        .GetSection(nameof(AppDatabaseConfiguration))
            .Get<AppDatabaseConfiguration>());

builder.Services.AddEmailerLayer();

builder.Services.AddApplicationLayer();

builder.Services.AddMessageBroker(c =>
{
    c.RegisterApplicationLayerAssembly(Assembly.GetExecutingAssembly());
    c.RegisterConnectionUri(builder.Configuration["RabbitMqConfiguration:ConnectionUri"]);
    c.RegisterListener<EmailCreatedEvent>();
});


//builder.Services.AddHostedService<SendWaitingEmailsService>();
//builder.Services.AddHostedService<NewEmailListenerService>();

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
