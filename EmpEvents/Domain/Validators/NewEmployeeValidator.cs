using EmpEvents.Domain.Employee;
using EmpEvents.Domain.Interfaces;

namespace EmpEvents.Domain.Validators;

public class NewEmployeeValidator : ValidatorExecutor<IEmployee>
{
    private NewEmployeeValidator() {}

    public static NewEmployeeValidator CreateInstance()
    {
        return new NewEmployeeValidator();
    }

    protected override bool Execute(IEmployee employee)
    {
        if (employee!.EmploymentEndDate.HasValue  
            && employee!.EmploymentStartDate > DateTime.Today)
            return true;

        return false;
    }
    
    public override string ToString()
    {
        return "NewEmployer";
    }

}