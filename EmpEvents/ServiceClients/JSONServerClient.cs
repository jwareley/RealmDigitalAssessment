using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using EmpEvents.Controllers;
using EmpEvents.Domain;
using EmpEvents.Domain.Employee;
using EmpEvents.ServiceClients.Interfaces;

namespace EmpEvents.ServiceClients;

public class JsonServerClient : IJsonServerClient
{
    private static HttpClient? _httpClient;

    public JsonServerClient()
    {
        RunAsync();
    }
    
    private Task RunAsync()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://interview-assessment-1.realmdigital.co.za/");
        return Task.CompletedTask;
    }

     public async Task<IEnumerable<EmployeeRecord?>?>? GetEmployees()
    {
        var response = await _httpClient?.GetAsync("employees")!;
       
        response.EnsureSuccessStatusCode();

       var employeeRecords = await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeRecord>>();

        return employeeRecords;
    }

     public async Task<IEnumerable<int?>?> GetDoNotNotifyEmployees()
     {
         var response = await _httpClient?.GetAsync("do-not-send-birthday-wishes")!;
       
         response.EnsureSuccessStatusCode();

         var employeeDoNotContactRecords = await response.Content.ReadFromJsonAsync<IEnumerable<int?>>();

         return employeeDoNotContactRecords!;
     }

     public void Dispose()
     {
         _httpClient?.CancelPendingRequests();
         _httpClient?.Dispose();
     }
}