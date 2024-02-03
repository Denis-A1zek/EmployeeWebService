namespace EmployeeWebService.Domain.Entities;

public record Employee : Identity
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int DepartmentId { get; set; }
    public Passport Passport { get; set; } = null!;
}
