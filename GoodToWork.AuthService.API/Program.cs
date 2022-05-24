using GoodToWork.AuthService.API.Grpc.Listeners;
using GoodToWork.AuthService.Application;
using GoodToWork.AuthService.Infrastructure.DependencyConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();
builder.Services.AddInfrastructure(builder.Configuration, typeof(ApplicationEntryPoint).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<ValidateTokenListener>();
});

app.Run();
