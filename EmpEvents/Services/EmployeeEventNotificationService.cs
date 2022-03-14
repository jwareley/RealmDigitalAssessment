using EmpEvents.Controllers.Interfaces;
using EmpEvents.ServiceClients.Interfaces;

namespace EmpEvents.Services;

public class EmployeeEventNotificationService : BackgroundService
{
    
    private readonly ILogger<EmployeeEventNotificationService> _logger;
    private readonly IJsonServerClient _jsonServerClient;
    private readonly INotificationServiceClient _notificationServiceClient;
    private readonly IEmployeeEventServiceController _employeeEventServiceController;
    
    private Task? _runEventTask = null;
    private CancellationTokenSource _cts;
    

    public EmployeeEventNotificationService(
        IJsonServerClient jsonServerClient,
        INotificationServiceClient notificationServiceClient,
        IEmployeeEventServiceController employeeEventServiceController,
        ILogger<EmployeeEventNotificationService> logger
        )
    {
        _logger = logger;
        _jsonServerClient = jsonServerClient;
        _notificationServiceClient = notificationServiceClient;
        _employeeEventServiceController = employeeEventServiceController;
    }


    public override Task StartAsync(CancellationToken cancelToken)
    {
        _logger.LogInformation("Employee Event Notification Service started ...");

        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancelToken);

        _runEventTask = ExecuteAsync(_cts.Token);

         return _runEventTask!.IsCompleted ? _runEventTask : Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        // Stop called without start
        if (_runEventTask == null)
        {
            return;
        }

        try
        {
            // Signal cancellation to the executing method
            _cts.Cancel();
        }
        finally
        {
            // Wait until the task completes or the stop token triggers
            await Task.WhenAny(_runEventTask, Task.Delay(Timeout.Infinite,
                cancellationToken));
        }
    }

    protected override async Task ExecuteAsync(CancellationToken cancelToken)
    {
        _logger.LogDebug($"Employee Event Notification Service is starting...");
        
        (cancelToken!).Register(() =>
            _logger.LogDebug($" Employee Event Notification Service background task is stopping."));
    
        while (!(cancelToken).IsCancellationRequested)
        {
            _employeeEventServiceController.StartAsync();
            
            _logger.LogDebug($"Employee Event Notification Service task doing background work.");
            
            _employeeEventServiceController.RunAsync();
        
            await Task.Delay(10000, (cancelToken));
        }
    
        _logger.LogDebug($"Employee Event Notification Service background task is stopping.");
    }
}