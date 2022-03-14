namespace EmpEvents.Domain.Interfaces;

public interface IEmployee
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? EmploymentStartDate { get; set; }
    public DateTime? EmploymentEndDate { get; set; }
    public DateTime? LastNotification { get; set; }
}