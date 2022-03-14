using EmpEvents.Domain.Employee;
using EmpEvents.Domain.Interfaces;

namespace EmpEvents.Domain.Validators;

public class ResignedEmployeeValidator : ValidatorExecutor<IEmployee>
{
    private ResignedEmployeeValidator() {}

    public static ResignedEmployeeValidator CreateInstance()
    {
        return new ResignedEmployeeValidator();
    }

    public ResignedEmployeeValidator(IEmployee? employee)
    {
        employee = employee;
    }

    protected override bool Execute(IEmployee employee)
    {
        if (employee!.EmploymentEndDate.HasValue 
            && employee!.EmploymentEndDate.Value < DateTime.Now.Date)
            return true;

        return false;
    }
    
    public override string ToString()
    {
        return "ResignedEmployee";
    }

}