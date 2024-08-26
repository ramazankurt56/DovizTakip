using DefaultCorsPolicyNugetPackage;
using DovizTakipServer.Application;
using DovizTakipServer.Application.Hubs;
using DovizTakipServer.Infrastructure;
using DovizTakipServer.WebAPI.Middlewares;
using DovizTakipServer.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// CORS Policy Configuration
builder.Services.AddControllers();
builder.Services.AddDefaultCors();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR();

builder.Services.AddHostedService<AutoDovizBackgroundService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.MapHub<TakipHub>("/takip-hub");

app.Run();
