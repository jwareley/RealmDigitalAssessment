using EmpEvents.Domain.Employee;

namespace EmpEvents.ServiceClients.Interfaces;

public interface IJsonServerClient : IDisposable
{
    Task<IEnumerable<EmployeeRecord?>?>? GetEmployees();
    Task<IEnumerable<int?>?>? GetDoNotNotifyEmployees();
}