using EmpEvents.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmpEvents.ServiceClients.Interfaces;

public interface INotificationServiceClient : IDisposable
{
    public Task<IActionResult> Send(IEmployeeEventContent content);
}