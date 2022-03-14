using EmpEvents.Controllers;
using EmpEvents.Domain;
using EmpEvents.Domain.Interfaces;
using EmpEvents.ServiceClients.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmpEvents.ServiceClients;

public class NotificationServiceClient : INotificationServiceClient
{
    private static HttpClient? _httpClient;
    
    public string NotificationTarget { get; set; } 
    
    public Task RunAsync()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://virtualmessagebroker/");
        return Task.CompletedTask;
    }

    [HttpPost("send/email/{content}")]
    public  Task<IActionResult> Send(IEmployeeEventContent content)
    {
       return Task.FromResult<IActionResult>(new ObjectResult(this) { StatusCode = StatusCodes.Status201Created });
    }
    
    public void Dispose()
    {
        _httpClient?.CancelPendingRequests();
        _httpClient?.Dispose();
    }
}