using EmployeeWebService.Domain.Entities;

namespace EmployeeWebService.Domain.Aggregates;

public record DepartmentAggregate
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public Company Company { get; set; } = null!;
}
