using EmpEvents.Domain.Interfaces;

namespace EmpEvents.Domain.Validators;

public class AlreadyNotifiedEmployeeValidator : ValidatorExecutor<IEmployee>
{
    public IEnumerable<int?> AlreadyNotifiedEmployeeList { get; set; }

    public static AlreadyNotifiedEmployeeValidator CreateInstance()
    {
        return new AlreadyNotifiedEmployeeValidator();
    }
    protected override bool Execute(IEmployee employee)
    {
        return AlreadyNotifiedEmployeeList
            .Select(alreadyNotifiedEmployeeId => alreadyNotifiedEmployeeId.Equals(employee!.Id)).FirstOrDefault();
    }

    public override string ToString()
    {
        return "AlreadyNotifiedValidator";
    }
}