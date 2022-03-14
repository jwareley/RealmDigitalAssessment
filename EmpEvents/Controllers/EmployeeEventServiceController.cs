using EmpEvents.Controllers.Interfaces;
using EmpEvents.Domain;
using EmpEvents.Domain.Employee;
using EmpEvents.Domain.Interfaces;
using EmpEvents.Domain.Notifications;
using EmpEvents.Domain.Validators;
using EmpEvents.ServiceClients;
using EmpEvents.ServiceClients.Interfaces;

namespace EmpEvents.Controllers;

public class EmployeeEventServiceController : IEmployeeEventServiceController
{
    private readonly ILogger<EmployeeEventServiceController> _logger;

    private readonly IJsonServerClient _jsonServerClient;
    private readonly INotificationServiceClient _notificationServiceClient;

    private List<ValidatorExecutor<IEmployee>> _validators;
    private IEnumerable<EmployeeRecord?>? _employeeRecords { get; set; }
    public IEnumerable<int?>? _doNotNotifyEmployeeRecords { get; set; }
    
    public List<int?>? _notificationSentList { get; set; }


    public EmployeeEventServiceController(
        ILogger<EmployeeEventServiceController> logger,
        IJsonServerClient jsonServerClient,
        INotificationServiceClient notificationServiceClient)
    {
        _logger = logger;
        _jsonServerClient = jsonServerClient;
        _notificationServiceClient = notificationServiceClient;
        _notificationSentList = new List<int?>();
    }

    public async void StartAsync()
    {
        _validators = new List<ValidatorExecutor<IEmployee>>
        {
            AlreadyNotifiedEmployeeValidator.CreateInstance(),
            DoNotNotifyEmployeeValidator.CreateInstance(),
            NewEmployeeValidator.CreateInstance(),
            ResignedEmployeeValidator.CreateInstance(),
            LeapYearEmployeeValidator.CreateInstance(),
            IdentifyEmployeeBirthdayValidator.CreateInstance()
        };
        
    }

    public async void RunAsync()
    {
        _employeeRecords = await _jsonServerClient.GetEmployees()!;
        _doNotNotifyEmployeeRecords = await _jsonServerClient.GetDoNotNotifyEmployees()!;
        

        foreach (var employeeRecord in _employeeRecords!)
        {
            var skipEmployee = false;
            
            if (employeeRecord == null) continue;
            _logger.LogInformation($"Employee: {employeeRecord.Id}");
            skipEmployee = ValidateEmployees(employeeRecord);

            if (skipEmployee)
                continue;

            await NotifyEmployee(employeeRecord);
        }
    }
    
    private bool ValidateEmployees(IEmployee? employeeRecord)
    {
        var skipEmployee = false;
        
        foreach (var validator in _validators)
        {
            var validatorExecutor = validator;
            GetValidatorType(ref validatorExecutor);
            
            if (!validatorExecutor.Validate(employeeRecord))
            {
                _logger.LogInformation($"\t {validatorExecutor}: false");
                skipEmployee = true;
            }
            else
            {
                _logger.LogInformation($"\t {validatorExecutor}: true");
            }
        }

        return skipEmployee;
    }

    private void GetValidatorType(ref ValidatorExecutor<IEmployee> validator)
    {
        if (validator is DoNotNotifyEmployeeValidator doNotNotifyEmployeeValidator)
        {
            doNotNotifyEmployeeValidator
                .DoNotNotifyEmployeeList = _doNotNotifyEmployeeRecords!;
        }
            
        if (validator is AlreadyNotifiedEmployeeValidator alreadyNotifiedValidator)
        {
            alreadyNotifiedValidator
                .AlreadyNotifiedEmployeeList = _notificationSentList!;
        }
    }

    
    private async Task NotifyEmployee(EmployeeRecord employeeRecord)
    {
        await _notificationServiceClient.Send(
            new BirthdayEventContent(employeeRecord.Name, employeeRecord.LastName));

        _logger.LogInformation($"Send Birthday Message => {employeeRecord.Id}");
        _notificationSentList?.Add(employeeRecord.Id);
    }
    
    public void ShutdownAsync()
    {
        _jsonServerClient.Dispose();
        _notificationServiceClient.Dispose();
    }

}