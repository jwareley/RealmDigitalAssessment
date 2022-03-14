using EmpEvents.Domain.Interfaces;

namespace EmpEvents.Domain.Employee;

public record LeapYearEmployeeRecord : EmployeeRecord
{
    public static LeapYearEmployeeRecord? CreateInstance(IEmployee employee)
    {
        if (employee.DateOfBirth!.Value.Day == 29
            && employee.DateOfBirth.Value.Month == 2)
        {
            if (!DateTime.IsLeapYear(DateTime.Now.Year))
            {
                employee.DateOfBirth =
                    new DateTime(employee.DateOfBirth.Value.Year, employee.DateOfBirth.Value.Month, 28);
            }
        }

        return (LeapYearEmployeeRecord) employee;
    }
}