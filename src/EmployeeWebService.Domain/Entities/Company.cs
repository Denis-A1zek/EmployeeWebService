namespace EmployeeWebService.Domain.Entities;

public record Company : Identity
{
    public string Name { get; set; } = null!;
}
