namespace EmployeeWebService.Domain.Entities;

public record Department : Identity
{
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int CompanyId { get; set; }
}
