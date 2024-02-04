using EmployeeWebService.Domain.Entities;

namespace EmployeeWebService.Domain.Aggregates;

public class EmployeeAggregate
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int? CompanyId { get; set; }
    public Department? Department { get; set; }
    public Passport Passport { get; set; } = null!;
}
