using GoodToWork.Shared.FileProvider;
using GoodToWork.Shared.FileProvider.Utilities;
using GoodToWork.UserSerice.API.ApiModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureFileProvider(
    Configuration
        .CreateWithPublicKey(builder.Configuration["FileProvider:PublicKey"])
        .WithPrivateKey(builder.Configuration["FileProvider:PrivateKey"])
        .WithUrlEndPoint(builder.Configuration["FileProvider:UrlEndPoint"])
        .Build());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPut("/update-user", (UpdateUserModel updateUserModel, IFileProvider fileProvider) =>
{
    throw new NotImplementedException();
    fileProvider.SaveImage(updateUserModel.ImageSrc, Guid.NewGuid().ToString());
    return "";
});

app.Run();