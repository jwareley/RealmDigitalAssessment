using EmpEvents.Domain.Interfaces;

namespace EmpEvents.Domain.Notifications
{
    public class BirthdayEventContent : IEmployeeEventContent
    {
        public int? Id { get; set; }
        private string? EmployeeName { get; set; }
        private string? EmployeeLastname { get; set; }
        private DateTime? EmployeeBirthday { get; set; }
    
    
        public BirthdayEventContent() {}

        public BirthdayEventContent(string? employeeName, string? employeeLastname)
        {
            EmployeeName = employeeName;
            EmployeeLastname = employeeLastname;
        }

        public string GetContent()
        {
            return $"Happy Birthday {EmployeeName} {EmployeeLastname}";
        }
    }
}
