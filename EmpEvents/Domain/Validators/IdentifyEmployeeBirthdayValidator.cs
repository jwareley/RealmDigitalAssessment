using EmpEvents.Domain.Employee;
using EmpEvents.Domain.Interfaces;

namespace EmpEvents.Domain.Validators;

public class IdentifyEmployeeBirthdayValidator : ValidatorExecutor<IEmployee>
{
    private IdentifyEmployeeBirthdayValidator()
    {
    }

    public static IdentifyEmployeeBirthdayValidator CreateInstance()
    {
        return new IdentifyEmployeeBirthdayValidator();
    }

    protected override bool Execute(IEmployee employee)
    {
        if (!employee.DateOfBirth.HasValue) return false;

       return employee.DateOfBirth.Value.Month.Equals(DateTime.Today.Month)
               && employee.DateOfBirth.Value.Day.Equals(DateTime.Today.Day);
    }
    
    public override string ToString()
    {
        return "IdentifierEmployeeBirthday";
    }
    
}