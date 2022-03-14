using EmpEvents.Domain.Employee;
using EmpEvents.Domain.Interfaces;

namespace EmpEvents.Domain.Validators;

public class DoNotNotifyEmployeeValidator : ValidatorExecutor<IEmployee>
{
    public IEnumerable<int?> DoNotNotifyEmployeeList { get; set; }

    public static DoNotNotifyEmployeeValidator CreateInstance()
    {
        return new DoNotNotifyEmployeeValidator();
    }
    protected override bool Execute(IEmployee employee)
    {
        return DoNotNotifyEmployeeList.Select(doNotDeployEmployeeId => doNotDeployEmployeeId.Equals(employee!.Id)).FirstOrDefault();
    }

    public override string ToString()
    {
        return "DoNotNotifyValidator";
    }
}