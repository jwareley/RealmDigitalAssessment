using EmpEvents.Domain.Interfaces;

namespace EmpEvents.Domain.Validators;

public class LeapYearEmployeeValidator : ValidatorExecutor<IEmployee>
{
    public static LeapYearEmployeeValidator CreateInstance()
    {
        return new LeapYearEmployeeValidator();
    }

    protected override bool Execute(IEmployee employee)
    {
        if (employee.DateOfBirth == null) return false;
        if (employee.DateOfBirth!.Value.Day == 29 
            && employee.DateOfBirth.Value.Month == 2)
        {
            if (!(DateTime.IsLeapYear(DateTime.Now.Year)
                 && DateTime.Now.Day == 28 
                 && DateTime.Now.Month == 2))
            {
                return true;
            }
            
            if ((DateTime.IsLeapYear(DateTime.Now.Year)
                  && DateTime.Now.Day == 29 
                  && DateTime.Now.Month == 2))
            {
                return true;
            }
        }

        return false;
    }

    public override string ToString()
    {
        return "LeapYearEmployeeValidator";
    }
}