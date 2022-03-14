using EmpEvents;
using EmpEvents.Controllers;
using EmpEvents.Controllers.Interfaces;
using EmpEvents.Domain;
using EmpEvents.ServiceClients;
using EmpEvents.ServiceClients.Interfaces;
using EmpEvents.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddDebug();
// Add services to the container
builder.Services.AddHostedService<EmployeeEventNotificationService>();

builder.Services.AddSingleton<IJsonServerClient, JsonServerClient>();
builder.Services.AddSingleton<INotificationServiceClient, NotificationServiceClient>();

builder.Services.AddSingleton<IEmployeeEventServiceController, EmployeeEventServiceController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.Run();